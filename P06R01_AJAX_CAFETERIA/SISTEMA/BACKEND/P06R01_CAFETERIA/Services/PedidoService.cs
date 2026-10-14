using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using P06R01_CAFETERIA.Data;
using P06R01_CAFETERIA.Dtos;
using P06R01_CAFETERIA.Models;

namespace P06R01_CAFETERIA.Services
{
    public class PedidoService
    {
        private const string EstadoInicial = "Pendiente";
        private readonly CafeteriaContext _db;

        public PedidoService(CafeteriaContext db)
        {
            _db = db;
        }

        public async Task<List<PedidoResumenDto>> ListarAsync() =>
            await _db.Pedidos.AsNoTracking()
                .OrderByDescending(p => p.PedidoID)
                .Select(p => new PedidoResumenDto(
                    p.PedidoID, p.Fecha, p.Total, p.Estado,
                    p.Cliente != null ? p.Cliente.Nombre : "",
                    p.Detalles.Sum(d => (int?)d.Cantidad) ?? 0))
                .ToListAsync();

        public async Task<PedidoDto?> ObtenerAsync(int id)
        {
            var p = await _db.Pedidos.AsNoTracking()
                .Include(x => x.Cliente)
                .Include(x => x.Detalles).ThenInclude(d => d.Producto)
                .FirstOrDefaultAsync(x => x.PedidoID == id);
            if (p is null) return null;

            var detalles = p.Detalles
                .OrderBy(d => d.DetalleID)
                .Select(d => new DetallePedidoDto(d.DetalleID, d.ProductoID, d.Producto?.Nombre ?? "",
                                                  d.Cantidad, d.PrecioUnitario, d.Cantidad * d.PrecioUnitario))
                .ToList();

            return new PedidoDto(p.PedidoID, p.Fecha, p.Total, p.Estado,
                                 p.ClienteID, p.Cliente?.Nombre ?? "", p.Cliente?.Email, p.Cliente?.Telefono,
                                 detalles);
        }

        // Crea Cliente (o reutiliza el del mismo correo) + Pedido + Detalles y descuenta el stock,
        // todo en una sola transacción: si algo falla no se guarda nada.
        public async Task<PedidoDto> CrearAsync(PedidoCrearDto dto)
        {
            // Si el mismo producto viene dos veces se suman las cantidades
            var lineas = dto.Productos
                .GroupBy(x => x.ProductoID)
                .Select(g => new { ProductoID = g.Key, Cantidad = g.Sum(x => x.Cantidad) })
                .ToList();

            await using var tx = await _db.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            // 1. Productos con bloqueo de actualización: nadie más puede gastar ese stock hasta el COMMIT
            var productos = new Dictionary<int, Producto>();
            foreach (var linea in lineas)
            {
                var producto = await _db.Productos
                    .FromSqlInterpolated($"SELECT * FROM Productos WITH (UPDLOCK, ROWLOCK) WHERE ProductoID = {linea.ProductoID}")
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                if (producto is null)
                    throw new ReglaNegocioException($"El producto {linea.ProductoID} no existe.", StatusCodes.Status404NotFound);

                var stock = producto.Stock ?? 0;
                if (stock < linea.Cantidad)
                    throw new ReglaNegocioException(
                        stock == 0
                            ? $"{producto.Nombre} está agotado."
                            : $"Solo quedan {stock} de {producto.Nombre}.",
                        StatusCodes.Status409Conflict);

                productos[linea.ProductoID] = producto;
            }

            // 2. Cliente: se reutiliza si ya existe ese correo (se actualizan nombre y teléfono)
            var email = dto.Cliente.Email.Trim().ToLowerInvariant();
            var cliente = await _db.Clientes.FirstOrDefaultAsync(c => c.Email == email);
            if (cliente is null)
            {
                cliente = new Cliente { Email = email };
                _db.Clientes.Add(cliente);
            }
            cliente.Nombre = dto.Cliente.Nombre.Trim();
            cliente.Telefono = string.IsNullOrWhiteSpace(dto.Cliente.Telefono) ? null : dto.Cliente.Telefono.Trim();
            await _db.SaveChangesAsync();

            // 3. Pedido con el SP spInsertarPedido (el total se calcula con los precios de la BD, no del navegador)
            var total = lineas.Sum(l => productos[l.ProductoID].Precio * l.Cantidad);
            var pedidoId = new SqlParameter("@NuevoPedidoID", SqlDbType.Int) { Direction = ParameterDirection.Output };
            await _db.Database.ExecuteSqlRawAsync(
                "EXEC spInsertarPedido @Cliente, @Total, @Estado, @NuevoPedidoID OUTPUT",
                new SqlParameter("@Cliente", cliente.ClienteID),
                new SqlParameter("@Total", SqlDbType.Decimal) { Precision = 10, Scale = 2, Value = total },
                new SqlParameter("@Estado", EstadoInicial),
                pedidoId);
            var nuevoPedidoId = (int)pedidoId.Value;

            foreach (var linea in lineas)
            {
                var producto = productos[linea.ProductoID];

                // 4. Detalle con el SP spInsertarDetallePedido
                var detalleId = new SqlParameter("@UltimoDetallePedido", SqlDbType.Int) { Direction = ParameterDirection.Output };
                await _db.Database.ExecuteSqlRawAsync(
                    "EXEC spInsertarDetallePedido @PedidoID, @ProductoID, @Cantidad, @PrecioUnitario, @UltimoDetallePedido OUTPUT",
                    new SqlParameter("@PedidoID", nuevoPedidoId),
                    new SqlParameter("@ProductoID", producto.ProductoID),
                    new SqlParameter("@Cantidad", linea.Cantidad),
                    new SqlParameter("@PrecioUnitario", SqlDbType.Decimal) { Precision = 10, Scale = 2, Value = producto.Precio },
                    detalleId);

                // 5. Descontar stock (pasa por TR_Auditoria_Instead_Update, que también lo audita)
                await _db.Database.ExecuteSqlInterpolatedAsync(
                    $"UPDATE Productos SET Stock = Stock - {linea.Cantidad} WHERE ProductoID = {producto.ProductoID}");
            }

            await tx.CommitAsync();
            return (await ObtenerAsync(nuevoPedidoId))!;
        }
    }
}
