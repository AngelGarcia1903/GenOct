using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamionesAPI.Models
{
    public class Camiones
    {
        [Key]
        public int IdCamion { get; set; }
        public string? Matricula { get; set; }      // VARCHAR(50) NULL
        public string? TipoCamion { get; set; }     // VARCHAR(50) NULL
        public int Modelo { get; set; }
        public string Marca { get; set; } = string.Empty;
        public int Capacidad { get; set; }
        public double Kilometraje { get; set; }
        public bool Disponibilidad { get; set; }
        public string UrlFoto { get; set; } = string.Empty;
    }
}
