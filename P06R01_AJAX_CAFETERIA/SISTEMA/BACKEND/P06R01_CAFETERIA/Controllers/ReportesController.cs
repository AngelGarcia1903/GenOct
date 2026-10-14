using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P06R01_CAFETERIA.Data;
using P06R01_CAFETERIA.Models;

namespace P06R01_CAFETERIA.Controllers
{
    [ApiController]
    [Route("api")]
    public class ReportesController : ControllerBase
    {
        private readonly CafeteriaContext _db;

        public ReportesController(CafeteriaContext db)
        {
            _db = db;
        }

        // GET api/reportes/resumen-ventas   (vista VWVistaResumenVentas)
        [HttpGet("reportes/resumen-ventas")]
        public async Task<ActionResult<List<ResumenVenta>>> ResumenVentas() =>
            await _db.ResumenVentas.AsNoTracking().OrderByDescending(r => r.MontoTotal).ToListAsync();

        // GET api/auditoria?top=50   (lo que registran los triggers de Productos)
        [HttpGet("auditoria")]
        public async Task<ActionResult<List<AuditoriaProducto>>> Auditoria([FromQuery] int top = 50) =>
            await _db.AuditoriaProductos.AsNoTracking()
                .OrderByDescending(a => a.AuditoriaID)
                .Take(Math.Clamp(top, 1, 500))
                .ToListAsync();
    }
}
