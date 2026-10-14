using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class E_Camion
    {

        public int IdCamion { get; set; }
        public string Matricula { get; set; }
        public string TipoCamion { get; set; }
        public int Modelo { get; set; }
        public string Marca { get; set; }
        public int Capacidad { get; set; }
        public double Kilometraje { get; set; }
        public bool Disponibilidad { get; set; }
        public string UrlFoto { get; set; }
        //propiedad calculada para descripcion completa

        public string Descripcion => $"{TipoCamion} {Marca} {Modelo} ({Matricula})";




    }//fin de clase


}//fin de namespace
