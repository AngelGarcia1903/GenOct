using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using CapaEntidades;
using System.Data;


namespace CapaDatos
{
    public class D_Rutas
    {
        public List<E_Ruta> listarRutas()
        {
            List<E_Ruta> lista = new List<E_Ruta>();
            using (SqlConnection conn = Conexion.ObtenerConexion())
            {
                //Select_Rutas_Detalle no recibe parametros y no devuelve IdChofer, IdCamion ni FechaRegistro
                SqlCommand cmd = new SqlCommand("dbo.Select_Rutas_Detalle", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    lista.Add(new E_Ruta
                    {

                        IdRuta = Convert.ToInt32(dr["IdRuta"]),
                        Origen = dr["Origen"].ToString(),
                        FechaSalida = Convert.ToDateTime(dr["FechaSalida"]),
                        FechaLlegada = Convert.ToDateTime(dr["FechaLlegada"]),
                        ATiempo = Convert.ToBoolean(dr["ATiempo"]),
                        Distancia = Convert.ToDouble(dr["Distancia"]),
                        Destino = dr["Destino"].ToString(),
                        NombreChofer = dr["NombreChofer"].ToString(),
                        Licencia = dr["Licencia"].ToString(),
                        TelefonoChofer = dr["TelefonoChofer"].ToString(),
                        FotoChofer = dr["FotoChofer"].ToString(),
                        Matricula = dr["Matricula"].ToString(),
                        FotoCamion = dr["FotoCamion"].ToString()

                    });
                }
            }
            return lista;


        }

        public bool InsertarRutas(E_Ruta rutas)
        {
            using (SqlConnection conn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("dbo.Insert_Rutas", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdChofer", rutas.IdChofer);
                cmd.Parameters.AddWithValue("@IdCamion", rutas.IdCamion);
                cmd.Parameters.AddWithValue("@Origen", rutas.Origen);
                cmd.Parameters.AddWithValue("@Destino", rutas.Destino);
                cmd.Parameters.AddWithValue("@FechaSalida", rutas.FechaSalida);
                cmd.Parameters.AddWithValue("@FechaLlegada", rutas.FechaLlegada);
                cmd.Parameters.AddWithValue("@ATiempo", rutas.ATiempo);
                cmd.Parameters.AddWithValue("@Distancia", rutas.Distancia);
                foreach (SqlParameter p in cmd.Parameters)
                {
                    System.Diagnostics.Debug.WriteLine($"{p.ParameterName} = {p.Value}");
                }
                cmd.ExecuteNonQuery();
                return true; //Si no hubo excepcion, el sp se ejcuto correctamente

            }

        }


        public bool ActualizarRutas(E_Ruta rutas)
        {
            using (SqlConnection conn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("dbo.Update_Rutas", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdRuta", rutas.IdRuta);
                cmd.Parameters.AddWithValue("@IdChofer", rutas.IdChofer);
                cmd.Parameters.AddWithValue("@IdCamion", rutas.IdCamion);
                cmd.Parameters.AddWithValue("@Origen", rutas.Origen);
                cmd.Parameters.AddWithValue("@Destino", rutas.Destino);
                cmd.Parameters.AddWithValue("@FechaSalida", rutas.FechaSalida);
                cmd.Parameters.AddWithValue("@FechaLlegada", rutas.FechaLlegada);
                cmd.Parameters.AddWithValue("@ATiempo", rutas.ATiempo);
                cmd.Parameters.AddWithValue("@Distancia", rutas.Distancia);
                foreach (SqlParameter p in cmd.Parameters)
                {
                    System.Diagnostics.Debug.WriteLine($"{p.ParameterName}= {p.Value}");

                }
                cmd.ExecuteNonQuery();
                return true;

            }

        }

        public bool EliminarRutas(int idRuta)
        {
            using (SqlConnection conn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("dbo.Delete_Ruta", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdRuta", idRuta);
                cmd.ExecuteNonQuery();
                return true;
            }
        }

        public E_Ruta ObtenerRutaPorID(int idRuta)
        {
            using (SqlConnection conn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("dbo.Select_Rutas", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdRuta", idRuta);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    return new E_Ruta

                    {
                        IdRuta = Convert.ToInt32(dr["IdRuta"]),
                        IdChofer = Convert.ToInt32(dr["IdChofer"]),
                        IdCamion = Convert.ToInt32(dr["IdCamion"]),
                        Origen = dr["Origen"].ToString(),
                        FechaSalida = Convert.ToDateTime(dr["FechaSalida"]),
                        FechaLlegada = Convert.ToDateTime(dr["FechaLlegada"]),
                        ATiempo = Convert.ToBoolean(dr["ATiempo"]),
                        Distancia = Convert.ToDouble(dr["Distancia"]),
                        Destino = dr["Destino"].ToString(),
                        FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"]),
              
                    };

                }
                return null;

            }

        }
    }
}
