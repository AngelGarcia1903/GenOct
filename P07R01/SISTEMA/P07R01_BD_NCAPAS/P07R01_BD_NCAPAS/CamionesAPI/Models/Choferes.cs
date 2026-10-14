using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamionesAPI.Models
{
    public class Choferes
    {
        [Key]
        public int IdChofer { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string ApPaterno { get; set; } = string.Empty;
        public string ApMaterno { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
        public string Licencia { get; set; } = string.Empty;
        public string UrlFoto { get; set; } = string.Empty;
        public bool Disponibilidad { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]   // DEFAULT GETDATE(): la pone la BD
        public DateTime FechaRegistro { get; set; }
    }
}
