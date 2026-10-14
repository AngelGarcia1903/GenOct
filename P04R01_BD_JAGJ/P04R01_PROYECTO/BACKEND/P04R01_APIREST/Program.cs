using Microsoft.EntityFrameworkCore;
using P04R01_APIREST.Context;
using P04R01_APIREST.Services;

var builder = WebApplication.CreateBuilder(args);

//Agregar contexto de la base de datos
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("Conexion")));
// Add services to the container.

//Cliente HTTP para el servicio de códigos postales de Thona (URL en appsettings.json -> ThonaApi)
builder.Services.AddHttpClient<CodigoPostalService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ThonaApi:BaseUrl"]!);
    client.Timeout = TimeSpan.FromSeconds(15);
});

//Reglas de procesamiento de persona (Característica 2)
builder.Services.AddScoped<PersonaService>();

//CORS permitir peticiones desde angular (4200) y react/vite (5173)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200", "http://localhost:5173")
        .AllowAnyHeader()
        .AllowAnyMethod();

    });


}

);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngular");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
