using CitasApp.Data;
using CitasApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CitasApp.Presentation.Extensions
{
    // Antes: este bloque vivía dentro de Program.cs, mezclado con Identity,
    // el registro de repositorios y el pipeline HTTP (God File).
    // Ahora: una sola responsabilidad — registrar los DbContext de la app —
    // en su propio archivo.
    public static class PersistenceServiceExtensions
    {
        public static IServiceCollection AddPersistence(
            this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException(
                    "Connection string 'DefaultConnection' not found.");

            // DbContext de Identity (login)
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString));
            services.AddDatabaseDeveloperPageExceptionFilter();

            // DbContext para Pacientes/Medicos/Citas (Postgres)
            services.AddDbContext<CitasDbContext>(options =>
                options.UseNpgsql(connectionString, b => b.MigrationsAssembly("CitasApp.Presentation")));

            return services;
        }
    }
}
