using System.ComponentModel.DataAnnotations;

namespace CamionesAPI.DTOs
{
    // Datos que el cliente manda en POST y PUT. No lleva IdChofer ni FechaRegistro.
    public class ChoferGuardarDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener mas de 100 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido paterno es obligatorio.")]
        [StringLength(100, ErrorMessage = "El apellido paterno no puede tener mas de 100 caracteres.")]
        public string ApPaterno { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido materno es obligatorio.")]
        [StringLength(100, ErrorMessage = "El apellido materno no puede tener mas de 100 caracteres.")]
        public string ApMaterno { get; set; } = string.Empty;

        [Required(ErrorMessage = "El telefono es obligatorio.")]
        [RegularExpression(@"^\d{7,15}$", ErrorMessage = "El telefono debe tener solo numeros, entre 7 y 15 digitos.")]
        public string Telefono { get; set; } = string.Empty;

        // DateTime? + [Required]: si no viene en el JSON responde 400 (con DateTime normal llegaria 0001-01-01).
        // La edad minima la valida ChoferService.
        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        public DateTime? FechaNacimiento { get; set; }

        [Required(ErrorMessage = "La licencia es obligatoria.")]
        [StringLength(50, ErrorMessage = "La licencia no puede tener mas de 50 caracteres.")]
        public string Licencia { get; set; } = string.Empty;

        [Required(ErrorMessage = "La URL de la foto es obligatoria.")]
        [StringLength(255, ErrorMessage = "La URL de la foto no puede tener mas de 255 caracteres.")]
        public string UrlFoto { get; set; } = string.Empty;

        public bool Disponibilidad { get; set; }
    }
}
