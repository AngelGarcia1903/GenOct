using Microsoft.Data.SqlClient;

namespace P06R01_CAFETERIA.Services
{
    // Convierte los THROW de los triggers en errores de negocio con su mensaje original
    public static class ErroresSql
    {
        public static ReglaNegocioException? Traducir(SqlException ex) => ex.Number switch
        {
            50001 => new ReglaNegocioException(ex.Message, StatusCodes.Status400BadRequest), // TR_Auditoria_Instead_Insert / Update
            50002 => new ReglaNegocioException(ex.Message, StatusCodes.Status409Conflict),   // TR_Auditoria_Instead_Delete
            _ => null
        };
    }
}
