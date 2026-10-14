namespace P06R01_CAFETERIA.Services
{
    // Error de negocio que se regresa al frontend con un mensaje claro (400 o 409)
    public class ReglaNegocioException : Exception
    {
        public int StatusCode { get; }

        public ReglaNegocioException(string mensaje, int statusCode = StatusCodes.Status400BadRequest) : base(mensaje)
        {
            StatusCode = statusCode;
        }
    }
}
