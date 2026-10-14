using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamionesAPI.Models
{
    public class Rutas
    {
        [Key]
        public int IdRuta { get; set; }
        public int IdChofer { get; set; }    // FK -> Choferes
        public int IdCamion { get; set; }    // FK -> Camiones
        public string Origen { get; set; } = string.Empty;
        public string Destino { get; set; } = string.Empty;
        public DateTime FechaSalida { get; set; }
        public DateTime FechaLlegada { get; set; }
        public bool ATiempo { get; set; }
        public double Distancia { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]   // DEFAULT GETDATE(): la pone la BD
        public DateTime FechaRegistro { get; set; }
    }
}
