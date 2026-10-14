using CamionesAPI.DTOs;
using CamionesAPI.Models;

namespace CamionesAPI.Services.Interfaces
{
    // Contrato de la logica de negocio de Camiones. El controlador depende de esta interfaz,
    // no de la clase: Program.cs decide que implementacion se inyecta (AddScoped).
    public interface ICamionService
    {
        Task<ResultadoPaginado<Camiones>> ObtenerTodosAsync(bool? disponibilidad, PaginacionParametros paginacion);
        Task<Camiones> ObtenerPorIdAsync(int id);
        Task<Camiones> CrearAsync(CamionGuardarDto dto);
        Task ActualizarAsync(int id, CamionGuardarDto dto);
        Task EliminarAsync(int id);
    }
}
