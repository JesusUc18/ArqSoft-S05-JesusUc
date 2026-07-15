// CitasApp.Infrastructure/Data/CitasDbContext.cs
// DbContext de EF Core para las entidades de negocio (Paciente, Medico, Cita).
// Es independiente del ApplicationDbContext (ese es solo para Identity/login).

using CitasApp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CitasApp.Infrastructure.Data
{
    public class CitasDbContext : DbContext
    {
        public CitasDbContext(DbContextOptions<CitasDbContext> options) : base(options)
        {
        }

        public DbSet<Paciente> Pacientes => Set<Paciente>();
        public DbSet<Medico> Medicos => Set<Medico>();
        public DbSet<Cita> Citas => Set<Cita>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Paciente>().ToTable("Pacientes");
            modelBuilder.Entity<Medico>().ToTable("Medicos");
            modelBuilder.Entity<Cita>().ToTable("Citas");
        }
    }
}
