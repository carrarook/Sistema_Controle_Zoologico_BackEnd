using Microsoft.EntityFrameworkCore;
using Sistema_Controle_Zoologico.Models;

namespace Sistema_Controle_Zoologico.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Animal> Animais { get; set; }
        public DbSet<Cuidado> Cuidados { get; set; }
        public DbSet<AnimalCuidado> AnimalCuidados { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração de relacionamento Muitos-para-Muitos entre Animal e Cuidado
            modelBuilder.Entity<AnimalCuidado>()
                .HasKey(ac => new { ac.AnimalId, ac.CuidadoId });

            modelBuilder.Entity<AnimalCuidado>()
                .HasOne(ac => ac.Animal)
                .WithMany(a => a.AnimalCuidados)
                .HasForeignKey(ac => ac.AnimalId);

            modelBuilder.Entity<AnimalCuidado>()
                .HasOne(ac => ac.Cuidado)
                .WithMany(c => c.AnimalCuidados)
                .HasForeignKey(ac => ac.CuidadoId);
        }
    }
}
