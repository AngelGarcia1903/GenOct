using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class E_Chofer
    {

        public int IdChofer { get; set; }
       public string Nombre { get; set; }
        public string  ApPaterno { get; set; }
        public string  ApMaterno { get; set; }
        public string  Telefono { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Licencia { get; set; }
        public string UrlFoto { get; set; }
        public bool Disponibilidad { get; set; }
        public DateTime FechaRegistro { get; set; }
        //Interpolacion de string propiedad calculada

        public string NombreCompleto => $"{Nombre} {ApPaterno} {ApMaterno}";
    }
}
