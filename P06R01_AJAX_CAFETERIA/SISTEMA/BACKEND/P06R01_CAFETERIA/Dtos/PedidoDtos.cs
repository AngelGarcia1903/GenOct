using System.ComponentModel.DataAnnotations;

namespace P06R01_CAFETERIA.Dtos
{
    // ----- Entrada: lo que manda el formulario "Tu pedido" del frontend -----
    public class ClienteDatosDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 100 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo no es válido.")]
        [StringLength(100, ErrorMessage = "El correo admite máximo 100 caracteres.")]
        public string Email { get; set; } = string.Empty;

        [RegularExpression(@"^\d{10}$", ErrorMessage = "El teléfono debe tener 10 dígitos.")]
        public string? Telefono { get; set; }
    }

    public class PedidoProductoDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "Producto inválido.")]
        public int ProductoID { get; set; }

        [Range(1, 50, ErrorMessage = "La cantidad debe estar entre 1 y 50.")]
        public int Cantidad { get; set; }
    }

    public class PedidoCrearDto
    {
        [Required(ErrorMessage = "Faltan los datos del cliente.")]
        public ClienteDatosDto Cliente { get; set; } = new();

        [Required(ErrorMessage = "Agrega al menos un producto.")]
        [MinLength(1, ErrorMessage = "Agrega al menos un producto.")]
        public List<PedidoProductoDto> Productos { get; set; } = new();
    }

    // ----- Salida -----
    public record DetallePedidoDto(int DetalleID, int? ProductoID, string Producto, int Cantidad, decimal PrecioUnitario, decimal Subtotal);

    public record PedidoDto(int PedidoID, DateTime? Fecha, decimal Total, string? Estado,
                            int? ClienteID, string Cliente, string? Email, string? Telefono,
                            List<DetallePedidoDto> Detalles);

    public record PedidoResumenDto(int PedidoID, DateTime? Fecha, decimal Total, string? Estado, string Cliente, int Articulos);

    public record ClienteDto(int ClienteID, string Nombre, string? Email, string? Telefono);
}
