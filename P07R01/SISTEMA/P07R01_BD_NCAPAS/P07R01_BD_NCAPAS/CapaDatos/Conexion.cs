using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class Conexion
    {
        //Cadena de conexion
        private static string cadenaConexion =
            "Server=Angelgar;Database=GenOctCCR;Trusted_Connection=True;";
        //Método para obtener la cadena de conexión
        public static SqlConnection ObtenerConexion()
        {
            //ADO.NET
            SqlConnection conn = new SqlConnection(cadenaConexion);
            try
            {
                conn.Open();
                return conn;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la cadena de conexion: " + ex.Message);
            }
        }//fin del metodo de obtener conexion

        public static bool ProbarConexion()
        {
            try
            {
                using (SqlConnection conn = ObtenerConexion())
                {
                    return conn.State == System.Data.ConnectionState.Open;
                }
            }
            catch
            {
                return false;

            }

        }//fin metodo probar conexion
        
    }//fin de clase
}//fin del namespace