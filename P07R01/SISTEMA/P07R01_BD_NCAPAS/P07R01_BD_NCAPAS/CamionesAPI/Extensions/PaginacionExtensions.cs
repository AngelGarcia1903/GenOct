using CamionesAPI.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CamionesAPI.Extensions
{
    // Metodo de extension: agrega .PaginarAsync(...) a cualquier consulta de EF,
    // para no repetir el mismo codigo de Skip/Take en cada servicio.
    public static class PaginacionExtensions
    {
        public static async Task<ResultadoPaginado<T>> PaginarAsync<T>(
            this IQueryable<T> consulta, PaginacionParametros parametros)
        {
            int total = await consulta.CountAsync();   // SELECT COUNT(*) con los mismos filtros

            var datos = await consulta
                .Skip((parametros.Pagina - 1) * parametros.Tamano)   // salta las paginas anteriores
                .Take(parametros.Tamano)                              // toma solo esta pagina
                .ToListAsync();

            return new ResultadoPaginado<T>
            {
                Datos = datos,
                PaginaActual = parametros.Pagina,
                TamanoPagina = parametros.Tamano,
                TotalRegistros = total,
                TotalPaginas = (int)Math.Ceiling(total / (double)parametros.Tamano)
            };
        }
    }
}
