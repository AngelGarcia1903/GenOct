using CamionesAPI.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CamionesAPI.Middleware
{
    // Manejo global de errores: atrapa cualquier excepcion que salga de un controlador o servicio
    // y responde siempre con el mismo formato JSON (ProblemDetails). Asi los controladores
    // no necesitan try/catch. Se registra en Program.cs con AddExceptionHandler + UseExceptionHandler.
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly IProblemDetailsService _problemDetails;
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(IProblemDetailsService problemDetails, ILogger<GlobalExceptionHandler> logger)
        {
            _problemDetails = problemDetails;
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var (status, titulo, detalle) = exception switch
            {
                NoEncontradoException ex => (StatusCodes.Status404NotFound, "No encontrado", ex.Message),
                ConflictoException ex => (StatusCodes.Status409Conflict, "Conflicto", ex.Message),
                ReglaNegocioException ex => (StatusCodes.Status400BadRequest, "Datos invalidos", ex.Message),

                // La BD rechazo el cambio (llave foranea, valor duplicado...): es un conflicto con los datos.
                DbUpdateException => (StatusCodes.Status409Conflict, "Conflicto con la base de datos",
                    "La base de datos rechazo la operacion porque choca con otros registros."),

                // Cualquier otro error: 500 sin mostrar detalles internos al cliente.
                _ => (StatusCodes.Status500InternalServerError, "Error interno del servidor",
                    "Ocurrio un error inesperado. Intenta de nuevo mas tarde.")
            };

            if (status == StatusCodes.Status500InternalServerError)
                _logger.LogError(exception, "Error no controlado en {Ruta}", httpContext.Request.Path);
            else
                _logger.LogWarning("{Titulo} en {Ruta}: {Mensaje}", titulo, httpContext.Request.Path, exception.Message);

            httpContext.Response.StatusCode = status;

            return await _problemDetails.TryWriteAsync(new ProblemDetailsContext
            {
                HttpContext = httpContext,
                Exception = exception,
                ProblemDetails = new ProblemDetails
                {
                    Status = status,
                    Title = titulo,
                    Detail = detalle
                }
            });
        }
    }
}
