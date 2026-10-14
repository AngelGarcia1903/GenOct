namespace P06R01_CAFETERIA.Models
{
    // Vista dbo.VWVistaResumenVentas (solo lectura, sin llave)
    public class ResumenVenta
    {
        public int ClienteID { get; set; }
        public string Cliente { get; set; } = string.Empty;
        public string? Email { get; set; }
        public int? TotalPedidos { get; set; }
        public decimal MontoTotal { get; set; }
        public DateTime? UltimaCompra { get; set; }
    }
}
