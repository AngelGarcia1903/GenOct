using System.ComponentModel.DataAnnotations;

namespace CamionesAPI.DTOs
{
    // Datos que el cliente manda en POST y PUT. No lleva IdCamion: el id lo genera
    // el IDENTITY (POST) o viene en la URL (PUT).
    public class CamionGuardarDto
    {
        [Required(ErrorMessage = "La matricula es obligatoria.")]
        [StringLength(50, ErrorMessage = "La matricula no puede tener mas de 50 caracteres.")]
        public string Matricula { get; set; } = string.Empty;

        [Required(ErrorMessage = "El tipo de camion es obligatorio.")]
        [StringLength(50, ErrorMessage = "El tipo de camion no puede tener mas de 50 caracteres.")]
        public string TipoCamion { get; set; } = string.Empty;

        // El tope real (año actual + 1) lo valida CamionService.
        [Range(1900, 2100, ErrorMessage = "El modelo debe estar entre 1900 y 2100.")]
        public int Modelo { get; set; }

        [Required(ErrorMessage = "La marca es obligatoria.")]
        [StringLength(50, ErrorMessage = "La marca no puede tener mas de 50 caracteres.")]
        public string Marca { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "La capacidad debe ser mayor a 0.")]
        public int Capacidad { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El kilometraje no puede ser negativo.")]
        public double Kilometraje { get; set; }

        public bool Disponibilidad { get; set; }

        [Required(ErrorMessage = "La URL de la foto es obligatoria.")]
        [StringLength(255, ErrorMessage = "La URL de la foto no puede tener mas de 255 caracteres.")]
        public string UrlFoto { get; set; } = string.Empty;
    }
}
