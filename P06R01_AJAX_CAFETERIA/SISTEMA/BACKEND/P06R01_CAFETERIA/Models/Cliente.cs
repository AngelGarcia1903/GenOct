namespace P06R01_CAFETERIA.Models
{
    // Tabla dbo.Clientes
    public class Cliente
    {
        public int ClienteID { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Telefono { get; set; }

        public List<Pedido> Pedidos { get; set; } = new();
    }
}
