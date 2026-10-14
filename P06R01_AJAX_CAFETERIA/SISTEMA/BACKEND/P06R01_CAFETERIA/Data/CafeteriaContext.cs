using Microsoft.EntityFrameworkCore;
using P06R01_CAFETERIA.Models;

namespace P06R01_CAFETERIA.Data
{
    public class CafeteriaContext : DbContext
    {
        public CafeteriaContext(DbContextOptions<CafeteriaContext> options) : base(options) { }

        public DbSet<Producto> Productos => Set<Producto>();
        public DbSet<Cliente> Clientes => Set<Cliente>();
        public DbSet<Pedido> Pedidos => Set<Pedido>();
        public DbSet<DetallePedido> DetallesPedido => Set<DetallePedido>();
        public DbSet<AuditoriaProducto> AuditoriaProductos => Set<AuditoriaProducto>();
        public DbSet<ResumenVenta> ResumenVentas => Set<ResumenVenta>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>(e =>
            {
                // Declarar los triggers hace que EF no use OUTPUT en sus UPDATE/DELETE
                // (SQL Server no permite OUTPUT sin INTO en tablas con triggers).
                e.ToTable("Productos", tb =>
                {
                    tb.HasTrigger("TR_AuditarCambioPrecio");
                    tb.HasTrigger("TR_Auditoria_After_Insert");
                    tb.HasTrigger("TR_Auditoria_Instead_Insert");
                    tb.HasTrigger("TR_Auditoria_Instead_Update");
                    tb.HasTrigger("TR_Auditoria_After_Delete");
                    tb.HasTrigger("TR_Auditoria_Instead_Delete");
                });
                e.HasKey(p => p.ProductoID);
                e.Property(p => p.Nombre).HasMaxLength(100).IsRequired();
                e.Property(p => p.Categoria).HasMaxLength(50).IsRequired();
                e.Property(p => p.Precio).HasColumnType("decimal(10,2)");
            });

            modelBuilder.Entity<Cliente>(e =>
            {
                e.ToTable("Clientes");
                e.HasKey(c => c.ClienteID);
                e.Property(c => c.Nombre).HasMaxLength(100).IsRequired();
                e.Property(c => c.Email).HasMaxLength(100);
                e.Property(c => c.Telefono).HasMaxLength(15);
            });

            modelBuilder.Entity<Pedido>(e =>
            {
                e.ToTable("Pedidos");
                e.HasKey(p => p.PedidoID);
                e.Property(p => p.Total).HasColumnType("decimal(10,2)");
                e.Property(p => p.Estado).HasMaxLength(20);
                e.HasOne(p => p.Cliente).WithMany(c => c.Pedidos).HasForeignKey(p => p.ClienteID);
            });

            modelBuilder.Entity<DetallePedido>(e =>
            {
                e.ToTable("DetallesPedido");
                e.HasKey(d => d.DetalleID);
                e.Property(d => d.PrecioUnitario).HasColumnType("decimal(10,2)");
                e.HasOne(d => d.Pedido).WithMany(p => p.Detalles).HasForeignKey(d => d.PedidoID);
                e.HasOne(d => d.Producto).WithMany().HasForeignKey(d => d.ProductoID);
            });

            modelBuilder.Entity<AuditoriaProducto>(e =>
            {
                e.ToTable("AuditoriaProductos");
                e.HasKey(a => a.AuditoriaID);
                e.Property(a => a.PrecioAnterior).HasColumnType("decimal(10,2)");
                e.Property(a => a.PrecioNuevo).HasColumnType("decimal(10,2)");
            });

            modelBuilder.Entity<ResumenVenta>(e =>
            {
                e.HasNoKey();
                e.ToView("VWVistaResumenVentas");
                e.Property(r => r.MontoTotal).HasColumnType("decimal(10,2)");
            });
        }
    }
}
