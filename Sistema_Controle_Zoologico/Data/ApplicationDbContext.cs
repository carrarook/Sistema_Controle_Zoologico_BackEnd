using Microsoft.EntityFrameworkCore;
using Sistema_Controle_Zoologico.Models;

namespace Sistema_Controle_Zoologico.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Animal> Animais { get; set; }
        public DbSet<Cuidado> Cuidados { get; set; }
        public DbSet<AnimalCuidado> AnimalCuidados { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração da relação muitos-para-muitos entre Animal e Cuidado
            modelBuilder.Entity<AnimalCuidado>()
                .HasKey(ac => new { ac.AnimalId, ac.CuidadoId });

            modelBuilder.Entity<AnimalCuidado>()
                .HasOne(ac => ac.Animal)
                .WithMany(a => a.AnimalCuidados)
                .HasForeignKey(ac => ac.AnimalId)
                .OnDelete(DeleteBehavior.Restrict); // Previne exclusão em cascata

            modelBuilder.Entity<AnimalCuidado>()
                .HasOne(ac => ac.Cuidado)
                .WithMany(c => c.AnimalCuidados)
                .HasForeignKey(ac => ac.CuidadoId)
                .OnDelete(DeleteBehavior.Restrict); // Previne exclusão em cascata

            // Configurações adicionais para entidades
            modelBuilder.Entity<Animal>()
                .HasIndex(a => a.Nome);

            modelBuilder.Entity<Cuidado>()
                .HasIndex(c => c.Nome);
        }
    }
}