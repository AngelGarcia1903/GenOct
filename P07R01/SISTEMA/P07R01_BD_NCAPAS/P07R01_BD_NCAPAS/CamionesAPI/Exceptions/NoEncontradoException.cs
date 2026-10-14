namespace CamionesAPI.Exceptions
{
    // El registro pedido no existe. GlobalExceptionHandler la convierte en 404 Not Found.
    public class NoEncontradoException : Exception
    {
        public NoEncontradoException(string mensaje) : base(mensaje) { }
    }
}
