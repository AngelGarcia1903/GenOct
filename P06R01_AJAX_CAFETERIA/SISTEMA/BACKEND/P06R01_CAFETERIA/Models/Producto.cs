namespace P06R01_CAFETERIA.Models
{
    // Tabla dbo.Productos
    public class Producto
    {
        public int ProductoID { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int? Stock { get; set; }
        public DateTime? FechaCreacion { get; set; }
    }
}
