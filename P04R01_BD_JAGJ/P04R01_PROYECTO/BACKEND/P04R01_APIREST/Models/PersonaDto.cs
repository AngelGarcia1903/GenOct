namespace P04R01_APIREST.Models
{
    //Lo que manda el frontend para procesar a una persona (Característica 2)
    public class PersonaRequest
    {
        public string? nombreCompleto { get; set; }
        public string? hobby { get; set; }
        public DateTime? fechaNacimiento { get; set; }
        public string? email { get; set; }
    }//fin de la clase

    //Resultado del procesamiento
    public class PersonaResultado
    {
        public string nombreNormalizado { get; set; } = string.Empty;
        public string nombres { get; set; } = string.Empty;
        public string apellidoPaterno { get; set; } = string.Empty;
        public string apellidoMaterno { get; set; } = string.Empty;
        public string idUsuario { get; set; } = string.Empty;
        public string hobby { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string fechaNacimiento { get; set; } = string.Empty; //yyyy-MM-dd
        public int edad { get; set; }
        public List<string> preposiciones { get; set; } = new();
        public List<string> avisos { get; set; } = new(); //notas que no impiden el proceso (ej. caracteres eliminados)
    }//fin de la clase

    //Lo que manda el frontend al presionar "Finalizar" (Característica 3)
    public class FinalizarRequest
    {
        public PersonaRequest? persona { get; set; }
        public string? cp { get; set; }
        public string? codColonia { get; set; }
    }//fin de la clase

    //Registro guardado en TBLUsuarios
    public class RegistroDto
    {
        public int idRegistro { get; set; }
        public DateTime? fechaRegistro { get; set; }
        public PersonaResultado persona { get; set; } = new();
        public string cp { get; set; } = string.Empty;
        public string estado { get; set; } = string.Empty;
        public string codEstado { get; set; } = string.Empty;
        public string municipio { get; set; } = string.Empty;
        public string colonia { get; set; } = string.Empty;
        public string codColonia { get; set; } = string.Empty;
    }//fin de la clase

    //Errores de validación: { "mensaje": "...", "errores": { "email": "...", ... } }
    public class ValidacionDto
    {
        public string mensaje { get; set; } = "Revisa los datos capturados.";
        public Dictionary<string, string> errores { get; set; } = new();
    }//fin de la clase
}//fin del namespace
