using Microsoft.EntityFrameworkCore;

namespace MesonConnect.Models
{
    public class ApplicationDbContext : DbContext
    {
        // El constructor es necesario para que .NET pase la configuración de la conexión
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Aquí registrarás tus tablas más adelante. Ejemplo:
        // public DbSet<Usuario> Usuarios { get; set; }
    }
}