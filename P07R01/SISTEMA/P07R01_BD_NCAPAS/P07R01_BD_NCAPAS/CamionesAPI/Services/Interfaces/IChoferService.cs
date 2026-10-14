using CamionesAPI.DTOs;
using CamionesAPI.Models;

namespace CamionesAPI.Services.Interfaces
{
    public interface IChoferService
    {
        Task<ResultadoPaginado<Choferes>> ObtenerTodosAsync(bool? disponibilidad, PaginacionParametros paginacion);
        Task<Choferes> ObtenerPorIdAsync(int id);
        Task<Choferes> CrearAsync(ChoferGuardarDto dto);
        Task ActualizarAsync(int id, ChoferGuardarDto dto);
        Task EliminarAsync(int id);
    }
}
