using System.Xml.Linq;
using P04R01_APIREST.Models;

namespace P04R01_APIREST.Services
{
    //Posibles resultados de la consulta, para que el controlador elija el código HTTP correcto
    public enum ResultadoConsultaCP
    {
        Ok,
        SinInformacion,   //el servicio respondió, pero sin colonias (CP inexistente)
        ErrorServicio,    //el servicio respondió con error o con un formato que no se entiende
        SinComunicacion   //no se pudo conectar o tardó demasiado
    }

    public class ConsultaCPResultado
    {
        public ResultadoConsultaCP resultado { get; set; }
        public CodigoPostalDto? datos { get; set; }
        public string mensaje { get; set; } = string.Empty;
    }

    //Consume el servicio de Thona: GET {BaseUrl}/api/CatalogoCP?CP={cp}
    //El HttpClient lo crea e inyecta ASP.NET (AddHttpClient en Program.cs)
    public class CodigoPostalService
    {
        private readonly HttpClient http;
        private readonly ILogger<CodigoPostalService> logger;

        public CodigoPostalService(HttpClient http, ILogger<CodigoPostalService> logger)
        {
            this.http = http;
            this.logger = logger;
        }

        //Regla del PDF: solo números y exactamente 5 dígitos
        public static bool EsCPValido(string? cp)
        {
            return cp != null && cp.Length == 5 && cp.All(char.IsAsciiDigit);
        }

        public async Task<ConsultaCPResultado> ConsultarAsync(string cp)
        {
            string contenido;
            try
            {
                var respuesta = await http.GetAsync($"api/CatalogoCP?CP={cp}");
                contenido = await respuesta.Content.ReadAsStringAsync();

                if (!respuesta.IsSuccessStatusCode)
                {
                    logger.LogWarning("Servicio CP respondió {Status}: {Contenido}", (int)respuesta.StatusCode, contenido);
                    return Error(ResultadoConsultaCP.ErrorServicio,
                        "El servicio de códigos postales respondió con un error. Intenta de nuevo más tarde.");
                }
            }
            catch (TaskCanceledException)
            {
                return Error(ResultadoConsultaCP.SinComunicacion,
                    "El servicio de códigos postales tardó demasiado en responder. Intenta de nuevo.");
            }
            catch (HttpRequestException ex)
            {
                logger.LogWarning(ex, "No se pudo conectar con el servicio CP");
                return Error(ResultadoConsultaCP.SinComunicacion,
                    "No se pudo conectar con el servicio de códigos postales. Revisa tu conexión a internet.");
            }

            return Interpretar(cp, contenido);
        }

        //Convierte el XML de Thona a nuestro DTO. Ejemplo de respuesta:
        //<DATA><DESCESTADO>..</DESCESTADO><CODESTADO>..</CODESTADO><CODMUNICIPIO>..</CODMUNICIPIO>
        //      <DESCMUNICIPIO>..</DESCMUNICIPIO><COLONIAS><COLONIA ID="0"><DESCRIPCION_COLONIA>..</DESCRIPCION_COLONIA>
        //      <CODIGO_COLONIA>..</CODIGO_COLONIA></COLONIA></COLONIAS></DATA>
        //CP inexistente: <DATA><COLONIAS /></DATA>
        private ConsultaCPResultado Interpretar(string cp, string contenido)
        {
            if (string.IsNullOrWhiteSpace(contenido))
            {
                return Error(ResultadoConsultaCP.SinInformacion, $"No se encontró información para el código postal {cp}.");
            }

            XElement raiz;
            try
            {
                raiz = XDocument.Parse(contenido.Trim()).Root!;
            }
            catch (System.Xml.XmlException ex)
            {
                logger.LogWarning(ex, "Respuesta del servicio CP no es XML válido: {Contenido}", contenido);
                return Error(ResultadoConsultaCP.ErrorServicio,
                    "El servicio de códigos postales regresó una respuesta que no se pudo leer.");
            }

            var colonias = raiz.Element("COLONIAS")?.Elements("COLONIA")
                .Select(c => new ColoniaDto
                {
                    codigo = c.Element("CODIGO_COLONIA")?.Value.Trim() ?? string.Empty,
                    descripcion = c.Element("DESCRIPCION_COLONIA")?.Value.Trim() ?? string.Empty
                })
                .Where(c => c.descripcion != string.Empty)
                .ToList() ?? new List<ColoniaDto>();

            var estado = raiz.Element("DESCESTADO")?.Value.Trim() ?? string.Empty;

            if (colonias.Count == 0 || estado == string.Empty)
            {
                return Error(ResultadoConsultaCP.SinInformacion, $"No se encontró información para el código postal {cp}.");
            }

            return new ConsultaCPResultado
            {
                resultado = ResultadoConsultaCP.Ok,
                datos = new CodigoPostalDto
                {
                    cp = cp,
                    estado = estado,
                    codEstado = raiz.Element("CODESTADO")?.Value.Trim() ?? string.Empty,
                    municipio = raiz.Element("DESCMUNICIPIO")?.Value.Trim() ?? string.Empty,
                    codMunicipio = raiz.Element("CODMUNICIPIO")?.Value.Trim() ?? string.Empty,
                    colonias = colonias
                }
            };
        }

        private static ConsultaCPResultado Error(ResultadoConsultaCP resultado, string mensaje)
        {
            return new ConsultaCPResultado { resultado = resultado, mensaje = mensaje };
        }
    }//fin de la clase
}//fin del namespace
