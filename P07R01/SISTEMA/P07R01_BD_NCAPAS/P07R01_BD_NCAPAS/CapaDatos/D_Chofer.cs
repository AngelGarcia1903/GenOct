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
    public class D_Chofer
    {


        public List<E_Chofer> listarChoferes(bool? disponibilidad = null)
        {
            List<E_Chofer> lista = new List<E_Chofer>();
            using (SqlConnection conn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("Select_Chofer", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (disponibilidad.HasValue)
                    cmd.Parameters.AddWithValue("@Disponibilidad", disponibilidad.Value);
                else
                    cmd.Parameters.AddWithValue("@Disponibilidad", DBNull.Value);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    lista.Add(new E_Chofer
                    {
                        IdChofer = Convert.ToInt32(dr["IdChofer"]),
                        Nombre = dr["Nombre"].ToString(),
                        ApPaterno = dr["ApPaterno"].ToString(),
                        ApMaterno = dr["ApMaterno"].ToString(),
                        Telefono = dr["Telefono"].ToString(),
                        FechaNacimiento = Convert.ToDateTime(dr["FechaNacimiento"]),
                        Licencia = dr["Licencia"].ToString(),
                        UrlFoto = dr["UrlFoto"].ToString(),
                        Disponibilidad = Convert.ToBoolean(dr["Disponibilidad"]),
                        FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"])

                    });


                }

            }
            return lista;


        }


        public bool InsertarChofer(E_Chofer chofer)
        {
            using (SqlConnection conn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("Insert_Choferes", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre", chofer.Nombre);
                cmd.Parameters.AddWithValue("@ApPaterno",chofer.ApPaterno);
                cmd.Parameters.AddWithValue("@ApMaterno", chofer.ApMaterno);
                cmd.Parameters.AddWithValue("@Telefono", chofer.Telefono);
                cmd.Parameters.AddWithValue("@FechaNacimiento", chofer.FechaNacimiento);
                cmd.Parameters.AddWithValue("@Licencia", chofer.Licencia);
                cmd.Parameters.AddWithValue("@Disponibilidad", chofer.Disponibilidad);
                cmd.Parameters.AddWithValue("@UrlFoto",
                    string.IsNullOrEmpty(chofer.UrlFoto) ? (object)DBNull.Value : chofer.UrlFoto);

                foreach (SqlParameter p in cmd.Parameters)
                {
                    System.Diagnostics.Debug.WriteLine($"{p.ParameterName} = {p.Value}");
                }
                cmd.ExecuteNonQuery();
                return true; //Si no hubo excepcion, el sp se ejcuto correctamente

            }

        }

        public bool ActualizarChofer(E_Chofer chofer)
        {
            using (SqlConnection conn = Conexion.ObtenerConexion())
            {

                SqlCommand cmd = new SqlCommand("dbo.Update_Choferes", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdChofer", chofer.IdChofer);
                cmd.Parameters.AddWithValue("@Nombre", chofer.Nombre);
                cmd.Parameters.AddWithValue("@ApPaterno", chofer.ApPaterno);
                cmd.Parameters.AddWithValue("@ApMaterno", chofer.ApMaterno);
                cmd.Parameters.AddWithValue("@Telefono", chofer.Telefono);
                cmd.Parameters.AddWithValue("@FechaNacimiento", chofer.FechaNacimiento);
                cmd.Parameters.AddWithValue("@Licencia", chofer.Licencia);
                cmd.Parameters.AddWithValue("@Disponibilidad", chofer.Disponibilidad);
                cmd.Parameters.AddWithValue("@UrlFoto",
                    string.IsNullOrEmpty(chofer.UrlFoto) ? (object)DBNull.Value : chofer.UrlFoto);
                foreach (SqlParameter p in cmd.Parameters)
                {
                    System.Diagnostics.Debug.WriteLine($"{p.ParameterName}= {p.Value}");

                }
                cmd.ExecuteNonQuery();
                return true;

            }

        }


        public bool EliminarChofer(int idChofer)
        {
            using (SqlConnection conn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("Delete_Chofer", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdChofer", idChofer);
                cmd.ExecuteNonQuery();
                return true;
            }
        }

        public bool ExisteLicencia(string licencia)
        {
            using (SqlConnection conn = Conexion.ObtenerConexion())
            {

                SqlCommand cmd = new SqlCommand("Existe_Licencia", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Licencia", licencia);
                int resultado = Convert.ToInt32(cmd.ExecuteScalar());

                return resultado > 0;
            }
        }

        public E_Chofer ObtenerChoferPorID(int idChofer)
        {
            //La BD no tiene un SP para buscar un chofer por ID,
            //asi que se reutiliza Select_Chofer (todos) y se filtra por el ID
            return listarChoferes().FirstOrDefault(c => c.IdChofer == idChofer);

        }


    }//fin de la clase
}//fin de namespace
