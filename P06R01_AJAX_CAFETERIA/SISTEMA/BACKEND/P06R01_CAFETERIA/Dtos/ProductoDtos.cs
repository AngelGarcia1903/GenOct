using System.ComponentModel.DataAnnotations;

namespace P06R01_CAFETERIA.Dtos
{
    // Lo que la API devuelve de un producto
    public record ProductoDto(int ProductoID, string Nombre, string Categoria, decimal Precio, int Stock, DateTime? FechaCreacion);

    // Producto con las unidades vendidas (para "favoritos" del inicio)
    public record ProductoPopularDto(int ProductoID, string Nombre, string Categoria, decimal Precio, int Stock, int Vendidos);

    // Lo que la API recibe para crear o editar un producto
    public class ProductoGuardarDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre admite máximo 100 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "La categoría es obligatoria.")]
        [StringLength(50, ErrorMessage = "La categoría admite máximo 50 caracteres.")]
        public string Categoria { get; set; } = string.Empty;

        [Range(0.01, 99999999.99, ErrorMessage = "El precio debe ser mayor a 0.")]
        public decimal Precio { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo.")]
        public int Stock { get; set; }
    }
}
