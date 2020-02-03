using System.Diagnostics.CodeAnalysis;
using Isen.Dotnet.Library.Model;
using Microsoft.EntityFrameworkCore;

namespace Isen.Dotnet.Library.Context
{    
    public class ApplicationDbContext : DbContext
    {        
        // Listes des classes modèle / tables
        public DbSet<Person> PersonCollection { get; set; }
        public DbSet<Service> ServiceCollection {get;set;}

        public ApplicationDbContext(
            [NotNullAttribute] DbContextOptions options) : 
            base(options) {  }

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Tables et relations
            modelBuilder
                // Associer la classe Person...
                .Entity<Person>()
                // ... à la table Person
                .ToTable(nameof(Person))
                .HasKey(p => p.Id);

            modelBuilder.Entity<Person>()
                .HasOne(p => p.Service)
                .WithMany()
                .HasForeignKey(p => p.ServiceId);

           
            // Pareil pour Role
            // modelBuilder
            //     .Entity<Role>()
            //     .ToTable(nameof(Role))
            //     .HasKey(r => r.Id);

            // Pareil pour Service
            modelBuilder
                .Entity<Service>()
                .ToTable(nameof(Service))
                .HasKey(s => s.Id);
        }

    }
}