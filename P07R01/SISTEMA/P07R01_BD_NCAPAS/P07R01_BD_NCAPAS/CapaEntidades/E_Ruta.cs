using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class E_Ruta
    {

        public int IdRuta { get; set; }
        public int IdChofer { get; set; }
        public int IdCamion { get; set; }
        public string Origen { get; set; }
        public DateTime FechaSalida { get; set; }
        public DateTime FechaLlegada { get; set; }
        public bool ATiempo { get; set; }
        public double Distancia { get; set; }
        public string Destino { get; set; }
        public DateTime FechaRegistro { get; set; }
        //propiedades adicionales para el join
        public string NombreChofer { get; set; }
        public string Licencia { get; set; }
        public string TelefonoChofer { get; set; }
        public string FotoChofer { get; set; }
        public string Matricula { get; set; }
        public string FotoCamion { get; set; }



    }//fin de clase
}//fin del namespace
