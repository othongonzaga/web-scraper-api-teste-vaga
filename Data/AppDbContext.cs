using Microsoft.EntityFrameworkCore;
using WebScraperTesteAPI.Models;

namespace WebScraperTesteAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Alimento> Alimentos { get; set; }
        public DbSet<Componente> Componentes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alimento>()
                .HasMany(a => a.Componentes)
                .WithOne(c => c.Alimento)
                .HasForeignKey(c => c.AlimentoId);
        }
    }
}
