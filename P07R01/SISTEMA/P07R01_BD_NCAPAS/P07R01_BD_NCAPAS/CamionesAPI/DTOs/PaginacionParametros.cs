using System.ComponentModel.DataAnnotations;

namespace CamionesAPI.DTOs
{
    // Parametros de paginacion que llegan por la URL: api/Camiones?pagina=2&tamano=10
    // Si no se mandan, se usan los valores por defecto (pagina 1, 10 registros).
    public class PaginacionParametros
    {
        public const int TamanoMaximo = 100;

        [Range(1, int.MaxValue, ErrorMessage = "La pagina debe ser 1 o mayor.")]
        public int Pagina { get; set; } = 1;

        [Range(1, TamanoMaximo, ErrorMessage = "El tamano debe estar entre 1 y 100.")]
        public int Tamano { get; set; } = 10;
    }
}
