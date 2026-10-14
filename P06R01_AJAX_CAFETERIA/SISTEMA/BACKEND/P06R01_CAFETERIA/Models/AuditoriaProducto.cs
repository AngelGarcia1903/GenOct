namespace P06R01_CAFETERIA.Models
{
    // Tabla dbo.AuditoriaProductos (la llenan los triggers de Productos)
    public class AuditoriaProducto
    {
        public int AuditoriaID { get; set; }
        public int? ProductoID { get; set; }
        public string? Accion { get; set; }
        public decimal? PrecioAnterior { get; set; }
        public decimal? PrecioNuevo { get; set; }
        public string? Usuario { get; set; }
        public DateTime? Fecha { get; set; }
    }
}
