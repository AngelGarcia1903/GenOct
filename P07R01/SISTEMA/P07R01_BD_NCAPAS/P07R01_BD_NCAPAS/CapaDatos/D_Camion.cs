using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using CapaEntidades;
using System.Data;
using System.Security.Cryptography.Pkcs;

namespace CapaDatos
{

    public class D_Camion
    {


        public List<E_Camion> listarCamiones(bool? disponibilidad = null)
        {
            List<E_Camion> lista = new List<E_Camion>();
            using (SqlConnection conn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("dbo.Listar_Camiones",conn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (disponibilidad.HasValue)
                    cmd.Parameters.AddWithValue("@Disponibilidad", disponibilidad.Value);
                else
                    cmd.Parameters.AddWithValue("@Disponibilidad", DBNull.Value);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    lista.Add(new E_Camion
                    {
                        IdCamion = Convert.ToInt32(dr["IdCamion"]),
                        Matricula = dr["Matricula"].ToString(),
                        TipoCamion = dr["TipoCamion"].ToString(),
                        Modelo = Convert.ToInt32(dr["Modelo"]),
                        Marca = dr["Marca"].ToString(),
                        Capacidad = Convert.ToInt32(dr["Capacidad"]),
                        Kilometraje = Convert.ToDouble(dr["Kilometraje"]),
                        Disponibilidad = Convert.ToBoolean(dr["Disponibilidad"]),
                        UrlFoto = dr["UrlFoto"].ToString(),

                    });
                        
                      
                }

            }
            return lista;


        }

        public bool InsertarCamion(E_Camion camion) 
        {
            using (SqlConnection conn = Conexion.ObtenerConexion()) 
            {
                SqlCommand cmd = new SqlCommand("Insert_Camion", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Matricula", camion.Matricula);
                cmd.Parameters.AddWithValue("@TipoCamion", camion.TipoCamion);
                cmd.Parameters.AddWithValue("@Modelo", camion.Modelo);
                cmd.Parameters.AddWithValue("@Marca", camion.Marca);
                cmd.Parameters.AddWithValue("@Capacidad", camion.Capacidad);
                cmd.Parameters.AddWithValue("@Kilometraje", camion.Kilometraje);
                cmd.Parameters.AddWithValue("@Disponibilidad", camion.Disponibilidad);
                cmd.Parameters.AddWithValue("@UrlFoto",
                    string.IsNullOrEmpty(camion.UrlFoto) ? (object)DBNull.Value : camion.UrlFoto);
                    
                foreach (SqlParameter p in cmd.Parameters)
                {
                    System.Diagnostics.Debug.WriteLine($"{p.ParameterName} = {p.Value}");
                }
                cmd.ExecuteNonQuery();
                return true; //Si no hubo excepcion, el sp se ejcuto correctamente

            }
                    
        }

        public bool ActualizarCamion(E_Camion camion)
        {
            using (SqlConnection conn = Conexion.ObtenerConexion())
            {

                //SqlCommand cmd = conn.CreateCommand();
                SqlCommand cmd = new SqlCommand("dbo.Update_Camion", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdCamion", camion.IdCamion);
                cmd.Parameters.AddWithValue("@Matricula", camion.Matricula);
                cmd.Parameters.AddWithValue("TipoCamion", camion.TipoCamion);
                cmd.Parameters.AddWithValue("@Modelo", camion.Modelo);
                cmd.Parameters.AddWithValue("@Marca", camion.Marca);
                cmd.Parameters.AddWithValue("@Capacidad", camion.Capacidad);
                cmd.Parameters.AddWithValue("@Kilometraje", camion.Kilometraje);
                cmd.Parameters.AddWithValue("@Disponibilidad", camion.Disponibilidad);
                cmd.Parameters.AddWithValue("@UrlFoto",
                    string.IsNullOrEmpty(camion.UrlFoto) ? (object)DBNull.Value : camion.UrlFoto);
                foreach (SqlParameter p in cmd.Parameters)
                {
                    System.Diagnostics.Debug.WriteLine($"{p.ParameterName}= {p.Value}");

                }
                cmd.ExecuteNonQuery();
                return true;
                    
            }

        }

        public bool EliminarCamion(int idCamion) 
        {
            using (SqlConnection conn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("Delete_Camion", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdCamion", idCamion);
                cmd.ExecuteNonQuery();
                return true;
            }
        }


        public bool ExisteMatricula(string matricula) 
        {
            using (SqlConnection conn = Conexion.ObtenerConexion()) 
            {

                SqlCommand cmd = new SqlCommand("Existe_Matricula", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Matricula", matricula);
                int resultado = Convert.ToInt32(cmd.ExecuteScalar());

                return resultado > 0;
            }
        }

        public E_Camion ObtenerCamionPorID(int idCamion)
        {
            using (SqlConnection conn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("Obtener_Camion_ID", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdCamion", idCamion);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    return new E_Camion

                    {
                        IdCamion = Convert.ToInt32(dr["IdCamion"]),
                        Matricula = dr["Matricula"].ToString(),
                        TipoCamion = dr["TipoCamion"].ToString(),
                        Modelo = Convert.ToInt32(dr["Modelo"]),
                        Marca = dr["Marca"].ToString(),
                        Capacidad = Convert.ToInt32(dr["Capacidad"]),
                        Kilometraje = Convert.ToDouble(dr["Kilometraje"]),
                        Disponibilidad = Convert.ToBoolean(dr["Disponibilidad"]),
                        UrlFoto = dr["UrlFoto"].ToString(),


                    };

                }
                return null;

            }

        }

    }//fin de clase
}//fin del namespace
