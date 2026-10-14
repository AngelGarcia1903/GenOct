using CamionesAPI.DTOs;
using CamionesAPI.Models;
using CamionesAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CamionesAPI.Controllers
{
    // El controlador solo recibe la peticion y responde. La logica esta en CamionService
    // y los errores (404, 409, 400) los convierte GlobalExceptionHandler.
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CamionesController : ControllerBase
    {
        private readonly ICamionService _service;

        public CamionesController(ICamionService service)
        {
            _service = service;
        }

        // GET: api/Camiones?pagina=1&tamano=10&disponibilidad=true  (todos los parametros son opcionales)
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResultadoPaginado<Camiones>>> GetTodos(
            [FromQuery] bool? disponibilidad, [FromQuery] PaginacionParametros paginacion)
        {
            return Ok(await _service.ObtenerTodosAsync(disponibilidad, paginacion));
        }

        // GET: api/Camiones/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Camiones>> GetPorId(int id)
        {
            return Ok(await _service.ObtenerPorIdAsync(id));
        }

        // POST: api/Camiones
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Camiones>> Crear(CamionGuardarDto dto)
        {
            var camion = await _service.CrearAsync(dto);
            return CreatedAtAction(nameof(GetPorId), new { id = camion.IdCamion }, camion);   // 201
        }

        // PUT: api/Camiones/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Actualizar(int id, CamionGuardarDto dto)
        {
            await _service.ActualizarAsync(id, dto);
            return NoContent();   // 204
        }

        // DELETE: api/Camiones/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Eliminar(int id)
        {
            await _service.EliminarAsync(id);
            return NoContent();   // 204
        }
    }
}
