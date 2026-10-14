using Microsoft.AspNetCore.Mvc;
using P04R01_APIREST.Context;
using P04R01_APIREST.Models;
using P04R01_APIREST.Services;

namespace P04R01_APIREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly PersonaService servicio;
        private readonly CodigoPostalService servicioCP;
        private readonly AppDbContext context;

        public PersonaController(PersonaService servicio, CodigoPostalService servicioCP, AppDbContext context)
        {
            this.servicio = servicio;
            this.servicioCP = servicioCP;
            this.context = context;
        }

        //POST: api/Persona/procesar
        //Aplica las reglas (normalizar, camel case, preposiciones, ID) y las validaciones. No guarda nada.
        [HttpPost("procesar")]
        public ActionResult<PersonaResultado> Procesar([FromBody] PersonaRequest? req)
        {
            if (req == null)
            {
                return BadRequest(new ErrorDto("No se recibió información de la persona."));
            }

            var (resultado, errores) = servicio.Procesar(req, DateTime.Today);
            if (resultado == null)
            {
                return BadRequest(new ValidacionDto { errores = errores });
            }
            return Ok(resultado);
        }

        //POST: api/Persona/finalizar
        //Vuelve a validar TODO en el servidor (no se confía en lo que mandó el navegador) y guarda en TBLUsuarios.
        [HttpPost("finalizar")]
        public async Task<ActionResult<RegistroDto>> Finalizar([FromBody] FinalizarRequest? req)
        {
            if (req?.persona == null)
            {
                return BadRequest(new ErrorDto("No se recibió información de la persona."));
            }

            //1) Persona
            var (persona, errores) = servicio.Procesar(req.persona, DateTime.Today);
            if (persona == null)
            {
                return BadRequest(new ValidacionDto { errores = errores });
            }

            //2) Código postal y colonia: se consultan de nuevo para confirmar que la colonia pertenece al CP
            if (!CodigoPostalService.EsCPValido(req.cp))
            {
                return BadRequest(new ErrorDto("Consulta un código postal válido antes de finalizar."));
            }
            var consulta = await servicioCP.ConsultarAsync(req.cp!);
            if (consulta.resultado != ResultadoConsultaCP.Ok)
            {
                var codigo = consulta.resultado == ResultadoConsultaCP.SinInformacion
                    ? StatusCodes.Status400BadRequest
                    : StatusCodes.Status503ServiceUnavailable;
                return StatusCode(codigo, new ErrorDto(consulta.mensaje));
            }
            var datosCP = consulta.datos!;
            var colonia = datosCP.colonias.FirstOrDefault(c => c.codigo == req.codColonia);
            if (colonia == null)
            {
                return BadRequest(new ErrorDto("Selecciona una colonia del código postal consultado."));
            }

            //3) Guardar
            var registro = new TBLUsuarios
            {
                nombre = persona.nombreNormalizado,
                hobby = persona.hobby,
                fechanacimiento = req.persona.fechaNacimiento!.Value.Date,
                email = persona.email,
                idUsuarioGenerado = persona.idUsuario,
                preposiciones = persona.preposiciones.Count > 0 ? string.Join(",", persona.preposiciones) : null,
                cp = datosCP.cp,
                codEstado = datosCP.codEstado,
                estado = datosCP.estado,
                municipio = datosCP.municipio,
                codColonia = colonia.codigo,
                colonia = colonia.descripcion
            };

            try
            {
                context.TBLUsuarios.Add(registro);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ErrorDto($"No se pudo guardar el registro en la base de datos: {ex.GetBaseException().Message}"));
            }

            return Ok(new RegistroDto
            {
                idRegistro = registro.idUsuarios,
                fechaRegistro = registro.fechaRegistro,
                persona = persona,
                cp = datosCP.cp,
                estado = datosCP.estado,
                codEstado = datosCP.codEstado,
                municipio = datosCP.municipio,
                colonia = colonia.descripcion,
                codColonia = colonia.codigo
            });
        }
    }//fin de la clase
}//fin del namespace
