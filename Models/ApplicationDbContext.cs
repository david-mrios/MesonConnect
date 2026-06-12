using Microsoft.EntityFrameworkCore;

namespace MesonConnect.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Platillo> Platillo { get; set; }
        public DbSet<CategoriaPlatillo> CategoriaPlatillo { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Testimonio> Testimonio { get; set; }
        public DbSet<Promocion> Promocion { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<DetallePedido> DetallePedidos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DetallePedido>()
                .ToTable("DetallePedidos", tb =>
                    tb.HasTrigger("InsertDetallePedido"));
        }
    }
}