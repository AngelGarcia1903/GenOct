using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class N_Ruta
    {


        private D_Rutas objDatos = new D_Rutas();

        public List<E_Ruta> ListarRutas()
        {

            try
            {
                return objDatos.listarRutas();

            }
            catch (Exception ex)
            {

                throw new Exception("Error en capa de negocios:" + ex.Message);
            }

        }


        public string InsertarRuta(E_Ruta ruta)
        {

            try
            {
                if (string.IsNullOrEmpty(ruta.Origen))
                    return "El origen es obligatoria";
                if (string.IsNullOrEmpty(ruta.Destino))
                    return "El Destino paterno es obligatorio";
                //
                if (ruta.Distancia <= 0)
                    return "La distancia debe ser mayor a 0";
                if (ruta.IdChofer <= 0)
                    return "Debes seleccionar un chofer";
                if (ruta.IdCamion <= 0)
                    return "Debe seleccionar un camion";
                if (objDatos.InsertarRutas(ruta))
                    return "Ok";
                else
                    return "No se puedo insertar la ruta";
            }
            catch (Exception ex)
            {
                return "Error:" + ex.Message;
            }

        }


        public string ActualizarRuta(E_Ruta ruta)
        {

            try
            {
                if (ruta.Distancia <= 0)
                    return "La distancia debe ser mayor a 0";
                if (objDatos.ActualizarRutas(ruta))
                    return "Ok";
                else
                    return "No se pudo actualizar la ruta";
            }
            catch (Exception ex)
            {
                return "Error" + ex.Message;
            }

        }


        public string EliminarRuta(int idRuta)
        {
            try
            {

                if (objDatos.EliminarRutas(idRuta))
                    return "Ok";
                else
                    return "No se pudo eliminar la ruta";
            }
            catch (Exception ex)
            {
                return "Error" + ex.Message;
            }
        }


        public E_Ruta ObtenerRutaPorID(int idRuta)
        {

            try
            {

                return objDatos.ObtenerRutaPorID(idRuta);

            }
            catch (Exception ex)
            {
                throw new Exception("Error" + ex.Message);

            }
        }

    }
}
