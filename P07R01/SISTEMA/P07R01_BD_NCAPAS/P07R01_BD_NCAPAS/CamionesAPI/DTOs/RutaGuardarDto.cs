using System.ComponentModel.DataAnnotations;

namespace CamionesAPI.DTOs
{
    // Datos que el cliente manda en POST y PUT. No lleva IdRuta ni FechaRegistro.
    public class RutaGuardarDto
    {
        // Que el chofer y el camion existan lo valida RutaService.
        [Range(1, int.MaxValue, ErrorMessage = "El IdChofer es obligatorio.")]
        public int IdChofer { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El IdCamion es obligatorio.")]
        public int IdCamion { get; set; }

        [Required(ErrorMessage = "El origen es obligatorio.")]
        [StringLength(200, ErrorMessage = "El origen no puede tener mas de 200 caracteres.")]
        public string Origen { get; set; } = string.Empty;

        [Required(ErrorMessage = "El destino es obligatorio.")]
        [StringLength(200, ErrorMessage = "El destino no puede tener mas de 200 caracteres.")]
        public string Destino { get; set; } = string.Empty;

        // Que la llegada no sea antes de la salida lo valida RutaService.
        [Required(ErrorMessage = "La fecha de salida es obligatoria.")]
        public DateTime? FechaSalida { get; set; }

        [Required(ErrorMessage = "La fecha de llegada es obligatoria.")]
        public DateTime? FechaLlegada { get; set; }

        public bool ATiempo { get; set; }

        [Range(0.1, double.MaxValue, ErrorMessage = "La distancia debe ser mayor a 0.")]
        public double Distancia { get; set; }
    }
}
