using CamionesAPI.DTOs;
using CamionesAPI.Models;

namespace CamionesAPI.Services.Interfaces
{
    public interface IRutaService
    {
        Task<ResultadoPaginado<Rutas>> ObtenerTodosAsync(PaginacionParametros paginacion);
        Task<Rutas> ObtenerPorIdAsync(int id);
        Task<Rutas> CrearAsync(RutaGuardarDto dto);
        Task ActualizarAsync(int id, RutaGuardarDto dto);
        Task EliminarAsync(int id);
    }
}
