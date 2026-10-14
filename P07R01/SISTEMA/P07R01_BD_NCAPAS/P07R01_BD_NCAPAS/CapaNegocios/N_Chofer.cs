using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidades;

namespace CapaNegocios
{
    public class N_Chofer
    {

        private D_Chofer objDatos = new D_Chofer();

        public List<E_Chofer> ListarChoferes(bool? disponibilidad = null)
        {

            try
            {
                return objDatos.listarChoferes(disponibilidad);

            }
            catch (Exception ex)
            {

                throw new Exception("Error en capa de negocios:" + ex.Message);
            }

        }



        public string InsertarChofer(E_Chofer chofer)
        {

            try
            {
                if (string.IsNullOrEmpty(chofer.Nombre))
                    return "La Nombre es obligatoria";
                if (string.IsNullOrEmpty(chofer.ApPaterno))
                    return "El apellido paterno es obligatorio";
                if (string.IsNullOrEmpty(chofer.ApMaterno))
                    return "El apellido materno es obligatorio";
                if (string.IsNullOrEmpty(chofer.Telefono))
                    return "El telefono es obligatorio";
                if (chofer.Telefono.Length != 10)
                    return "El telefono debe ser de 10 digitos";
                if (string.IsNullOrEmpty(chofer.Licencia))
                    return "La licencia es obligatoria";
                int edad = DateTime.Now.Year - chofer.FechaNacimiento.Year;
                if (edad < 18)
                    return "El chofer debe ser mayor a 18 años";
                if (objDatos.ExisteLicencia(chofer.Licencia))
                    return "Ya existe un chofer con esa licencia";
                if (objDatos.InsertarChofer(chofer))
                    return "Ok";
                else
                    return "No se puedo insertar el camion";

            }
            catch (Exception ex)
            {
                return "Error:" + ex.Message;
            }

        }


        public string ActualizarChofer(E_Chofer chofer)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(chofer.Nombre))
                    return "El nombre es obligatorio";
                if (chofer.Telefono.Length != 10)
                    return "El telefono debe tener 10 digitos";
                if (objDatos.ActualizarChofer(chofer))
                    return "Ok";
                else
                    return "No se pudo actualizar el chofer";
            }
            catch (Exception ex)
            {
                return "Error" + ex.Message;

            }
        }


        public string EliminarChofer(int idChofer)
        {
            try
            {

                if (objDatos.EliminarChofer(idChofer))
                    return "Ok";
                else
                    return "No se pudo eliminar el chofer";
            }
            catch (Exception ex)
            {
                return "Error"+ex.Message;
            }
        }


        public E_Chofer ObtenerChoferPorID(int idChofer)
        {

            try
            {

                return objDatos.ObtenerChoferPorID(idChofer);

            }
            catch (Exception ex)
            {
                throw new Exception("Error" + ex.Message);

            }
        }
    }
}
