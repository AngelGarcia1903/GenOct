namespace P06R01_CAFETERIA.Models
{
    // Tabla dbo.Pedidos
    public class Pedido
    {
        public int PedidoID { get; set; }
        public int? ClienteID { get; set; }
        public DateTime? Fecha { get; set; }
        public decimal Total { get; set; }
        public string? Estado { get; set; }

        public Cliente? Cliente { get; set; }
        public List<DetallePedido> Detalles { get; set; } = new();
    }
}
