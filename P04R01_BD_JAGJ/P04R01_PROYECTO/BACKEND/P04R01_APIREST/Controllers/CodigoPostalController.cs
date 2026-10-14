using Microsoft.AspNetCore.Mvc;
using P04R01_APIREST.Models;
using P04R01_APIREST.Services;

namespace P04R01_APIREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodigoPostalController : ControllerBase
    {
        private readonly CodigoPostalService servicio;

        public CodigoPostalController(CodigoPostalService servicio)
        {
            this.servicio = servicio;
        }

        //GET: api/CodigoPostal/06700
        [HttpGet("{cp}")]
        public async Task<ActionResult<CodigoPostalDto>> Get(string cp)
        {
            if (!CodigoPostalService.EsCPValido(cp))
            {
                return BadRequest(new ErrorDto("El código postal debe tener exactamente 5 dígitos numéricos."));
            }

            var consulta = await servicio.ConsultarAsync(cp);

            return consulta.resultado switch
            {
                ResultadoConsultaCP.Ok => Ok(consulta.datos),
                ResultadoConsultaCP.SinInformacion => NotFound(new ErrorDto(consulta.mensaje)),
                ResultadoConsultaCP.SinComunicacion => StatusCode(StatusCodes.Status503ServiceUnavailable, new ErrorDto(consulta.mensaje)),
                _ => StatusCode(StatusCodes.Status502BadGateway, new ErrorDto(consulta.mensaje))
            };
        }
    }//fin de la clase
}//fin del namespace
