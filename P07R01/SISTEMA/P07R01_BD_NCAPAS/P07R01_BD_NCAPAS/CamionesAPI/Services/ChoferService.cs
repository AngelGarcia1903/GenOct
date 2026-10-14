using CamionesAPI.Context;
using CamionesAPI.DTOs;
using CamionesAPI.Exceptions;
using CamionesAPI.Extensions;
using CamionesAPI.Models;
using CamionesAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CamionesAPI.Services
{
    // Logica de negocio de Choferes.
    public class ChoferService : IChoferService
    {
        private const int EdadMinima = 18;

        private readonly AppDbContext _context;

        public ChoferService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResultadoPaginado<Choferes>> ObtenerTodosAsync(bool? disponibilidad, PaginacionParametros paginacion)
        {
            var consulta = _context.Choferes.AsNoTracking();

            if (disponibilidad.HasValue)
                consulta = consulta.Where(c => c.Disponibilidad == disponibilidad.Value);

            return await consulta.OrderByDescending(c => c.IdChofer).PaginarAsync(paginacion);
        }

        public async Task<Choferes> ObtenerPorIdAsync(int id)
        {
            return await _context.Choferes.FindAsync(id)
                ?? throw new NoEncontradoException($"No se encontro ningun chofer con el id {id}.");
        }

        public async Task<Choferes> CrearAsync(ChoferGuardarDto dto)
        {
            await ValidarAsync(dto, idActual: null);

            var chofer = new Choferes();
            CopiarDatos(dto, chofer);

            _context.Choferes.Add(chofer);
            await _context.SaveChangesAsync();   // INSERT; llena IdChofer y lee FechaRegistro de la BD
            return chofer;
        }

        public async Task ActualizarAsync(int id, ChoferGuardarDto dto)
        {
            var chofer = await ObtenerPorIdAsync(id);   // 404 si no existe
            await ValidarAsync(dto, idActual: id);

            CopiarDatos(dto, chofer);
            await _context.SaveChangesAsync();   // UPDATE
        }

        public async Task EliminarAsync(int id)
        {
            var chofer = await ObtenerPorIdAsync(id);   // 404 si no existe

            if (await _context.Rutas.AnyAsync(r => r.IdChofer == id))
                throw new ConflictoException(
                    $"No se puede eliminar el chofer {id} porque tiene rutas asignadas. Elimina primero sus rutas.");

            _context.Choferes.Remove(chofer);
            await _context.SaveChangesAsync();   // DELETE
        }

        private async Task ValidarAsync(ChoferGuardarDto dto, int? idActual)
        {
            DateTime nacimiento = dto.FechaNacimiento!.Value.Date;   // [Required] ya garantizo que viene
            DateTime hoy = DateTime.Today;

            if (nacimiento > hoy)
                throw new ReglaNegocioException("La fecha de nacimiento no puede ser futura.");

            if (nacimiento > hoy.AddYears(-EdadMinima))
                throw new ReglaNegocioException($"El chofer debe tener al menos {EdadMinima} anios.");

            string licencia = dto.Licencia.Trim();
            bool licenciaRepetida = await _context.Choferes
                .AnyAsync(c => c.Licencia == licencia && c.IdChofer != idActual);

            if (licenciaRepetida)
                throw new ConflictoException($"Ya existe un chofer con la licencia {licencia}.");
        }

        private static void CopiarDatos(ChoferGuardarDto dto, Choferes chofer)
        {
            chofer.Nombre = dto.Nombre.Trim();
            chofer.ApPaterno = dto.ApPaterno.Trim();
            chofer.ApMaterno = dto.ApMaterno.Trim();
            chofer.Telefono = dto.Telefono.Trim();
            chofer.FechaNacimiento = dto.FechaNacimiento!.Value.Date;
            chofer.Licencia = dto.Licencia.Trim();
            chofer.UrlFoto = dto.UrlFoto.Trim();
            chofer.Disponibilidad = dto.Disponibilidad;
        }
    }
}
