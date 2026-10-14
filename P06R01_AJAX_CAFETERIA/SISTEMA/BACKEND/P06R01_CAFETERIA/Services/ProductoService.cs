using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using P06R01_CAFETERIA.Data;
using P06R01_CAFETERIA.Dtos;
using P06R01_CAFETERIA.Models;

namespace P06R01_CAFETERIA.Services
{
    public class ProductoService
    {
        private readonly CafeteriaContext _db;

        public ProductoService(CafeteriaContext db)
        {
            _db = db;
        }

        public static ProductoDto ADto(Producto p) =>
            new(p.ProductoID, p.Nombre, p.Categoria, p.Precio, p.Stock ?? 0, p.FechaCreacion);

        public async Task<List<ProductoDto>> ListarAsync(string? categoria)
        {
            var consulta = _db.Productos.AsNoTracking();
            if (!string.IsNullOrWhiteSpace(categoria))
                consulta = consulta.Where(p => p.Categoria == categoria);

            var productos = await consulta.OrderBy(p => p.Categoria).ThenBy(p => p.Nombre).ToListAsync();
            return productos.Select(ADto).ToList();
        }

        public Task<List<string>> CategoriasAsync() =>
            _db.Productos.AsNoTracking().Select(p => p.Categoria).Distinct().OrderBy(c => c).ToListAsync();

        // Los más pedidos según DetallesPedido
        public async Task<List<ProductoPopularDto>> PopularesAsync(int top)
        {
            var populares = await _db.Productos.AsNoTracking()
                .Select(p => new
                {
                    Producto = p,
                    Vendidos = _db.DetallesPedido.Where(d => d.ProductoID == p.ProductoID).Sum(d => (int?)d.Cantidad) ?? 0
                })
                .OrderByDescending(x => x.Vendidos).ThenBy(x => x.Producto.Nombre)
                .Take(top)
                .ToListAsync();

            return populares
                .Select(x => new ProductoPopularDto(x.Producto.ProductoID, x.Producto.Nombre, x.Producto.Categoria,
                                                    x.Producto.Precio, x.Producto.Stock ?? 0, x.Vendidos))
                .ToList();
        }

        public async Task<ProductoDto?> ObtenerAsync(int id)
        {
            var p = await _db.Productos.AsNoTracking().FirstOrDefaultAsync(x => x.ProductoID == id);
            return p is null ? null : ADto(p);
        }

        // El INSERT lo ejecuta TR_Auditoria_Instead_Insert, por eso SCOPE_IDENTITY() llega vacío;
        // el trigger deja el ID nuevo en SESSION_CONTEXT('UltimoProductoID') y aquí se lee.
        public async Task<ProductoDto> CrearAsync(ProductoGuardarDto dto)
        {
            var conexion = _db.Database.GetDbConnection();
            await _db.Database.OpenConnectionAsync();
            try
            {
                using var cmd = conexion.CreateCommand();
                cmd.CommandText = @"INSERT INTO Productos (Nombre, Categoria, Precio, Stock)
                                    VALUES (@Nombre, @Categoria, @Precio, @Stock);
                                    SELECT CAST(SESSION_CONTEXT(N'UltimoProductoID') AS INT);";
                cmd.Parameters.Add(new SqlParameter("@Nombre", dto.Nombre.Trim()));
                cmd.Parameters.Add(new SqlParameter("@Categoria", dto.Categoria.Trim()));
                cmd.Parameters.Add(new SqlParameter("@Precio", dto.Precio));
                cmd.Parameters.Add(new SqlParameter("@Stock", dto.Stock));

                var id = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                return (await ObtenerAsync(id))!;
            }
            catch (SqlException ex) when (ErroresSql.Traducir(ex) is { } error)
            {
                throw error;
            }
            finally
            {
                await _db.Database.CloseConnectionAsync();
            }
        }

        public async Task<ProductoDto?> ActualizarAsync(int id, ProductoGuardarDto dto)
        {
            var p = await _db.Productos.FirstOrDefaultAsync(x => x.ProductoID == id);
            if (p is null) return null;

            p.Nombre = dto.Nombre.Trim();
            p.Categoria = dto.Categoria.Trim();
            p.Precio = dto.Precio;
            p.Stock = dto.Stock;

            await GuardarAsync();
            return ADto(p);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var p = await _db.Productos.FirstOrDefaultAsync(x => x.ProductoID == id);
            if (p is null) return false;

            _db.Productos.Remove(p);
            await GuardarAsync();
            return true;
        }

        private async Task GuardarAsync()
        {
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sql && ErroresSql.Traducir(sql) is { } error)
            {
                throw error;
            }
        }
    }
}
