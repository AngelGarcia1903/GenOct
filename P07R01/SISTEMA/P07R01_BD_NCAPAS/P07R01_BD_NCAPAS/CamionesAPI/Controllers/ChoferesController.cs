using CamionesAPI.DTOs;
using CamionesAPI.Models;
using CamionesAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CamionesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ChoferesController : ControllerBase
    {
        private readonly IChoferService _service;

        public ChoferesController(IChoferService service)
        {
            _service = service;
        }

        // GET: api/Choferes?pagina=1&tamano=10&disponibilidad=true  (todos los parametros son opcionales)
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResultadoPaginado<Choferes>>> GetTodos(
            [FromQuery] bool? disponibilidad, [FromQuery] PaginacionParametros paginacion)
        {
            return Ok(await _service.ObtenerTodosAsync(disponibilidad, paginacion));
        }

        // GET: api/Choferes/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Choferes>> GetPorId(int id)
        {
            return Ok(await _service.ObtenerPorIdAsync(id));
        }

        // POST: api/Choferes
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Choferes>> Crear(ChoferGuardarDto dto)
        {
            var chofer = await _service.CrearAsync(dto);
            return CreatedAtAction(nameof(GetPorId), new { id = chofer.IdChofer }, chofer);   // 201
        }

        // PUT: api/Choferes/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Actualizar(int id, ChoferGuardarDto dto)
        {
            await _service.ActualizarAsync(id, dto);
            return NoContent();   // 204
        }

        // DELETE: api/Choferes/5
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
