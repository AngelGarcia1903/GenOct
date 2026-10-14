using Microsoft.EntityFrameworkCore;
using CamionesAPI.Context;
using CamionesAPI.Middleware;
using CamionesAPI.Services;
using CamionesAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// ===================== ZONA 1: SERVICIOS =====================

// 1) Base de datos
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("conexion")));

// 2) Capa de servicios: cuando un controlador pida la interfaz, se le entrega la clase.
//    Scoped = una instancia por peticion HTTP (igual que el DbContext).
builder.Services.AddScoped<ICamionService, CamionService>();
builder.Services.AddScoped<IChoferService, ChoferService>();
builder.Services.AddScoped<IRutaService, RutaService>();

// 3) Manejo global de errores (Middleware/GlobalExceptionHandler.cs)
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// 4) CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("PoliticaCamiones", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "http://localhost:4200")              // uno o varios, separados por coma
              .AllowAnyHeader()                                                      // permite Content-Type, Authorization...
              .AllowAnyMethod();                                                    // permite GET, POST, PUT, DELETE
    });
});

// 5) Controladores y Swagger
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ===================== ZONA 2: PIPELINE ======================

// Primero: atrapa las excepciones de todo lo que sigue y responde con ProblemDetails.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("PoliticaCamiones");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
