using Microsoft.EntityFrameworkCore;
using P04R01_APIREST.Models;

namespace P04R01_APIREST.Context
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        //----------Modelo--------Clase
        public DbSet<TBLUsuarios> TBLUsuarios
        { get; set; }
    
    }
}
