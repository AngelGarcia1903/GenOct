namespace CamionesAPI.Exceptions
{
    // Los datos tienen el formato correcto pero rompen una regla del negocio
    // (ej. llegada antes de la salida). GlobalExceptionHandler la convierte en 400 Bad Request.
    public class ReglaNegocioException : Exception
    {
        public ReglaNegocioException(string mensaje) : base(mensaje) { }
    }
}
