using Microsoft.AspNetCore.Mvc;
using P06R01_CAFETERIA.Dtos;
using P06R01_CAFETERIA.Services;

namespace P06R01_CAFETERIA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly PedidoService _pedidos;

        public PedidosController(PedidoService pedidos)
        {
            _pedidos = pedidos;
        }

        // GET api/pedidos
        [HttpGet]
        public async Task<ActionResult<List<PedidoResumenDto>>> Listar() =>
            await _pedidos.ListarAsync();

        // GET api/pedidos/5   (consulta de pedido en "Tu pedido")
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PedidoDto>> Obtener(int id)
        {
            var pedido = await _pedidos.ObtenerAsync(id);
            return pedido is null ? NotFound() : pedido;
        }

        // POST api/pedidos   (formulario "Tu pedido")
        [HttpPost]
        public async Task<ActionResult<PedidoDto>> Crear(PedidoCrearDto dto)
        {
            var pedido = await _pedidos.CrearAsync(dto);
            return CreatedAtAction(nameof(Obtener), new { id = pedido.PedidoID }, pedido);
        }
    }
}
