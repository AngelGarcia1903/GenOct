namespace P04R01_APIREST.Models
{
    //Respuesta que la API regresa al frontend (JSON), ya convertida desde el XML de Thona
    public class CodigoPostalDto
    {
        public string cp { get; set; } = string.Empty;
        public string estado { get; set; } = string.Empty;
        public string codEstado { get; set; } = string.Empty;
        public string municipio { get; set; } = string.Empty;
        public string codMunicipio { get; set; } = string.Empty;
        public List<ColoniaDto> colonias { get; set; } = new();
    }//fin de la clase

    public class ColoniaDto
    {
        public string codigo { get; set; } = string.Empty;
        public string descripcion { get; set; } = string.Empty;
    }//fin de la clase

    //Formato único para los errores que regresa la API: { "mensaje": "..." }
    public class ErrorDto
    {
        public string mensaje { get; set; } = string.Empty;

        public ErrorDto(string mensaje)
        {
            this.mensaje = mensaje;
        }
    }//fin de la clase
}//fin del namespace
