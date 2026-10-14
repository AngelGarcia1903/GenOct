using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace P06R01_CAFETERIA.Services
{
    // Convierte cualquier ReglaNegocioException en una respuesta JSON { title, status, detail }
    public class ReglaNegocioFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is not ReglaNegocioException ex) return;

            context.Result = new ObjectResult(new ProblemDetails
            {
                Title = "No se pudo completar la operación",
                Status = ex.StatusCode,
                Detail = ex.Message
            })
            { StatusCode = ex.StatusCode };
            context.ExceptionHandled = true;
        }
    }
}
