using CamionesAPI.Context;
using CamionesAPI.DTOs;
using CamionesAPI.Exceptions;
using CamionesAPI.Extensions;
using CamionesAPI.Models;
using CamionesAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CamionesAPI.Services
{
    // Logica de negocio de Camiones: reglas, validaciones que necesitan la BD y acceso a datos.
    // Cuando algo no se cumple lanza una excepcion; GlobalExceptionHandler la convierte en 400/404/409.
    public class CamionService : ICamionService
    {
        private readonly AppDbContext _context;

        public CamionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResultadoPaginado<Camiones>> ObtenerTodosAsync(bool? disponibilidad, PaginacionParametros paginacion)
        {
            var consulta = _context.Camiones.AsNoTracking();

            if (disponibilidad.HasValue)
                consulta = consulta.Where(c => c.Disponibilidad == disponibilidad.Value);

            // Paginar requiere un orden fijo; si no, la misma pagina podria traer registros distintos.
            return await consulta.OrderByDescending(c => c.IdCamion).PaginarAsync(paginacion);
        }

        public async Task<Camiones> ObtenerPorIdAsync(int id)
        {
            return await _context.Camiones.FindAsync(id)
                ?? throw new NoEncontradoException($"No se encontro ningun camion con el id {id}.");
        }

        public async Task<Camiones> CrearAsync(CamionGuardarDto dto)
        {
            await ValidarAsync(dto, idActual: null);

            var camion = new Camiones();
            CopiarDatos(dto, camion);

            _context.Camiones.Add(camion);
            await _context.SaveChangesAsync();   // INSERT; llena IdCamion con el IDENTITY
            return camion;
        }

        public async Task ActualizarAsync(int id, CamionGuardarDto dto)
        {
            var camion = await ObtenerPorIdAsync(id);   // 404 si no existe
            await ValidarAsync(dto, idActual: id);

            CopiarDatos(dto, camion);
            await _context.SaveChangesAsync();   // UPDATE
        }

        public async Task EliminarAsync(int id)
        {
            var camion = await ObtenerPorIdAsync(id);   // 404 si no existe

            if (await _context.Rutas.AnyAsync(r => r.IdCamion == id))
                throw new ConflictoException(
                    $"No se puede eliminar el camion {id} porque tiene rutas asignadas. Elimina primero sus rutas.");

            _context.Camiones.Remove(camion);
            await _context.SaveChangesAsync();   // DELETE
        }

        // idActual = null al crear; al actualizar es el id del camion, para no compararlo consigo mismo.
        private async Task ValidarAsync(CamionGuardarDto dto, int? idActual)
        {
            int modeloMaximo = DateTime.Now.Year + 1;
            if (dto.Modelo > modeloMaximo)
                throw new ReglaNegocioException($"El modelo no puede ser mayor a {modeloMaximo}.");

            string matricula = dto.Matricula.Trim();
            bool matriculaRepetida = await _context.Camiones
                .AnyAsync(c => c.Matricula == matricula && c.IdCamion != idActual);

            if (matriculaRepetida)
                throw new ConflictoException($"Ya existe un camion con la matricula {matricula}.");
        }

        private static void CopiarDatos(CamionGuardarDto dto, Camiones camion)
        {
            camion.Matricula = dto.Matricula.Trim();
            camion.TipoCamion = dto.TipoCamion.Trim();
            camion.Modelo = dto.Modelo;
            camion.Marca = dto.Marca.Trim();
            camion.Capacidad = dto.Capacidad;
            camion.Kilometraje = dto.Kilometraje;
            camion.Disponibilidad = dto.Disponibilidad;
            camion.UrlFoto = dto.UrlFoto.Trim();
        }
    }
}
