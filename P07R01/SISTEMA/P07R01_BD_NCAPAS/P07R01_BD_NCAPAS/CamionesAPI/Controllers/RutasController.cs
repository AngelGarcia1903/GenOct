using CamionesAPI.DTOs;
using CamionesAPI.Models;
using CamionesAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CamionesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class RutasController : ControllerBase
    {
        private readonly IRutaService _service;

        public RutasController(IRutaService service)
        {
            _service = service;
        }

        // GET: api/Rutas?pagina=1&tamano=10  (los parametros son opcionales)
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResultadoPaginado<Rutas>>> GetTodos([FromQuery] PaginacionParametros paginacion)
        {
            return Ok(await _service.ObtenerTodosAsync(paginacion));
        }

        // GET: api/Rutas/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Rutas>> GetPorId(int id)
        {
            return Ok(await _service.ObtenerPorIdAsync(id));
        }

        // POST: api/Rutas
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Rutas>> Crear(RutaGuardarDto dto)
        {
            var ruta = await _service.CrearAsync(dto);
            return CreatedAtAction(nameof(GetPorId), new { id = ruta.IdRuta }, ruta);   // 201
        }

        // PUT: api/Rutas/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Actualizar(int id, RutaGuardarDto dto)
        {
            await _service.ActualizarAsync(id, dto);
            return NoContent();   // 204
        }

        // DELETE: api/Rutas/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Eliminar(int id)
        {
            await _service.EliminarAsync(id);
            return NoContent();   // 204
        }
    }
}
