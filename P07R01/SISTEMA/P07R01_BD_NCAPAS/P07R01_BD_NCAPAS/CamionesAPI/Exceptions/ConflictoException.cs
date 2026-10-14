namespace CamionesAPI.Exceptions
{
    // La operacion choca con datos existentes (duplicado, registro con dependencias).
    // GlobalExceptionHandler la convierte en 409 Conflict.
    public class ConflictoException : Exception
    {
        public ConflictoException(string mensaje) : base(mensaje) { }
    }
}
