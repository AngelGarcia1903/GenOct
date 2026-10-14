using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P06R01_CAFETERIA.Data;
using P06R01_CAFETERIA.Dtos;

namespace P06R01_CAFETERIA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly CafeteriaContext _db;

        public ClientesController(CafeteriaContext db)
        {
            _db = db;
        }

        // GET api/clientes
        [HttpGet]
        public async Task<ActionResult<List<ClienteDto>>> Listar() =>
            await _db.Clientes.AsNoTracking()
                .OrderBy(c => c.ClienteID)
                .Select(c => new ClienteDto(c.ClienteID, c.Nombre, c.Email, c.Telefono))
                .ToListAsync();

        // GET api/clientes/5   (usa el SP spObtenerClienteId)
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ClienteDto>> Obtener(int id)
        {
            var clientes = await _db.Clientes
                .FromSqlInterpolated($"EXEC spObtenerClienteId @Cliente = {id}")
                .AsNoTracking()
                .ToListAsync();

            var c = clientes.FirstOrDefault();
            return c is null ? NotFound() : new ClienteDto(c.ClienteID, c.Nombre, c.Email, c.Telefono);
        }
    }
}
