using Microsoft.AspNetCore.Mvc;
using P06R01_CAFETERIA.Dtos;
using P06R01_CAFETERIA.Services;

namespace P06R01_CAFETERIA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly ProductoService _productos;

        public ProductosController(ProductoService productos)
        {
            _productos = productos;
        }

        // GET api/productos?categoria=Bebidas   (Nuestra carta / Especiales)
        [HttpGet]
        public async Task<ActionResult<List<ProductoDto>>> Listar([FromQuery] string? categoria) =>
            await _productos.ListarAsync(categoria);

        // GET api/productos/categorias   (botones de filtro de la carta)
        [HttpGet("categorias")]
        public async Task<ActionResult<List<string>>> Categorias() =>
            await _productos.CategoriasAsync();

        // GET api/productos/populares?top=3   (favoritos del inicio)
        [HttpGet("populares")]
        public async Task<ActionResult<List<ProductoPopularDto>>> Populares([FromQuery] int top = 3) =>
            await _productos.PopularesAsync(Math.Clamp(top, 1, 20));

        // GET api/productos/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductoDto>> Obtener(int id)
        {
            var producto = await _productos.ObtenerAsync(id);
            return producto is null ? NotFound() : producto;
        }

        // POST api/productos
        [HttpPost]
        public async Task<ActionResult<ProductoDto>> Crear(ProductoGuardarDto dto)
        {
            var creado = await _productos.CrearAsync(dto);
            return CreatedAtAction(nameof(Obtener), new { id = creado.ProductoID }, creado);
        }

        // PUT api/productos/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<ProductoDto>> Actualizar(int id, ProductoGuardarDto dto)
        {
            var actualizado = await _productos.ActualizarAsync(id, dto);
            return actualizado is null ? NotFound() : actualizado;
        }

        // DELETE api/productos/5   (409 si el producto ya tiene pedidos)
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Eliminar(int id) =>
            await _productos.EliminarAsync(id) ? NoContent() : NotFound();
    }
}
