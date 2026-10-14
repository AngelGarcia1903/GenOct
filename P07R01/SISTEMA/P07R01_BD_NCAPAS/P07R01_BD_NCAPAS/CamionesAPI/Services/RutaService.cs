using CamionesAPI.Context;
using CamionesAPI.DTOs;
using CamionesAPI.Exceptions;
using CamionesAPI.Extensions;
using CamionesAPI.Models;
using CamionesAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CamionesAPI.Services
{
    // Logica de negocio de Rutas.
    public class RutaService : IRutaService
    {
        private readonly AppDbContext _context;

        public RutaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResultadoPaginado<Rutas>> ObtenerTodosAsync(PaginacionParametros paginacion)
        {
            return await _context.Rutas.AsNoTracking()
                .OrderByDescending(r => r.IdRuta)
                .PaginarAsync(paginacion);
        }

        public async Task<Rutas> ObtenerPorIdAsync(int id)
        {
            return await _context.Rutas.FindAsync(id)
                ?? throw new NoEncontradoException($"No se encontro ninguna ruta con el id {id}.");
        }

        public async Task<Rutas> CrearAsync(RutaGuardarDto dto)
        {
            await ValidarAsync(dto);

            var ruta = new Rutas();
            CopiarDatos(dto, ruta);

            _context.Rutas.Add(ruta);
            await _context.SaveChangesAsync();   // INSERT; llena IdRuta y lee FechaRegistro de la BD
            return ruta;
        }

        public async Task ActualizarAsync(int id, RutaGuardarDto dto)
        {
            var ruta = await ObtenerPorIdAsync(id);   // 404 si no existe
            await ValidarAsync(dto);

            CopiarDatos(dto, ruta);
            await _context.SaveChangesAsync();   // UPDATE
        }

        public async Task EliminarAsync(int id)
        {
            var ruta = await ObtenerPorIdAsync(id);   // 404 si no existe

            _context.Rutas.Remove(ruta);
            await _context.SaveChangesAsync();   // DELETE
        }

        private async Task ValidarAsync(RutaGuardarDto dto)
        {
            if (dto.FechaLlegada!.Value < dto.FechaSalida!.Value)
                throw new ReglaNegocioException("La fecha de llegada no puede ser anterior a la fecha de salida.");

            // Se valida aqui para responder 400 con un mensaje claro, en vez del error de llave foranea de la BD.
            if (!await _context.Choferes.AnyAsync(c => c.IdChofer == dto.IdChofer))
                throw new ReglaNegocioException($"No existe un chofer con el id {dto.IdChofer}.");

            if (!await _context.Camiones.AnyAsync(c => c.IdCamion == dto.IdCamion))
                throw new ReglaNegocioException($"No existe un camion con el id {dto.IdCamion}.");
        }

        private static void CopiarDatos(RutaGuardarDto dto, Rutas ruta)
        {
            ruta.IdChofer = dto.IdChofer;
            ruta.IdCamion = dto.IdCamion;
            ruta.Origen = dto.Origen.Trim();
            ruta.Destino = dto.Destino.Trim();
            ruta.FechaSalida = dto.FechaSalida!.Value;
            ruta.FechaLlegada = dto.FechaLlegada!.Value;
            ruta.ATiempo = dto.ATiempo;
            ruta.Distancia = dto.Distancia;
        }
    }
}
