using Microsoft.EntityFrameworkCore;
using P06R01_CAFETERIA.Data;
using P06R01_CAFETERIA.Services;

var builder = WebApplication.CreateBuilder(args);

// Base de datos GenOct (cadena "GenOct" en appsettings.json)
builder.Services.AddDbContext<CafeteriaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GenOct")));

builder.Services.AddScoped<ProductoService>();
builder.Services.AddScoped<PedidoService>();

builder.Services.AddControllers(options => options.Filters.Add<ReglaNegocioFilter>());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS: orígenes desde donde el frontend (jQuery $.ajax) puede llamar a la API.
// "null" es el origen que manda el navegador cuando se abre el HTML con doble clic (file://).
var origenes = builder.Configuration.GetSection("Cors:OrigenesPermitidos").Get<string[]>() ?? Array.Empty<string>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy => policy
        .WithOrigins(origenes)
        .AllowAnyHeader()
        .AllowAnyMethod());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Sin UseHttpsRedirection: el frontend llama por http://localhost:5220 y una redirección
// a https rompe las peticiones AJAX con CORS.
app.UseCors("Frontend");

app.UseAuthorization();

app.MapControllers();

app.Run();
