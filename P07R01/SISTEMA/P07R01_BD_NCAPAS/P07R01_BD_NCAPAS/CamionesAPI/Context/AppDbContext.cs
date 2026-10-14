using Microsoft.EntityFrameworkCore;   // (1) DbContext, DbSet, DbContextOptions
using CamionesAPI.Models;

namespace CamionesAPI.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Choferes> Choferes { get; set; }
        public DbSet<Camiones> Camiones { get; set; }
        public DbSet<Rutas> Rutas { get; set; }
    }
}
