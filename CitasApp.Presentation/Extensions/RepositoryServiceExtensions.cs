using CitasApp.Domain.Interfaces;
using CitasApp.Infrastructure.Repositories;

namespace CitasApp.Presentation.Extensions
{
    // Adapters de Pacientes/Medicos/Citas (Arquitectura Hexagonal).
    // AQUÍ se enchufa el Adapter que quieres usar para cada entidad.
    // Domain (los Ports/interfaces) NO se toca — solo cambia este método.
    //
    // Antes vivían aquí, comentados dentro de Program.cs, los bloques
    // alternativos (JSON, CSV, SQLite). Se documentan abajo para no perder
    // la referencia de cómo cambiar de adapter sin tener que adivinarlo.
    public static class RepositoryServiceExtensions
    {
        public static IServiceCollection AddDomainRepositories(this IServiceCollection services)
        {
            // ▶ Bloque A — JSON
            // services.AddSingleton<IPacienteRepository, JsonPacienteRepository>();
            // services.AddSingleton<IMedicoRepository, JsonMedicoRepository>();
            // services.AddSingleton<ICitaRepository, JsonCitaRepository>();

            // ▶ Bloque B — CSV (requiere las rutas de archivo, ver README)
            // services.AddSingleton<IPacienteRepository>(_ => new CsvPacienteRepository(csvPacientes));
            // services.AddSingleton<IMedicoRepository>  (_ => new CsvMedicoRepository(csvMedicos));
            // services.AddSingleton<ICitaRepository>    (_ => new CsvCitaRepository(csvCitas));

            // ▶ Bloque C — SQLite (requiere Microsoft.Data.Sqlite en Infrastructure)
            // services.AddSingleton<IPacienteRepository>(_ => new SqlitePacienteRepository(sqlitePath));
            // services.AddSingleton<IMedicoRepository>  (_ => new SqliteMedicoRepository(sqlitePath));
            // services.AddSingleton<ICitaRepository>    (_ => new SqliteCitaRepository(sqlitePath));

            // ▶ Bloque D — Postgres (EF Core / CitasDbContext) ← activo ahora
            services.AddScoped<IPacienteRepository, EfPacienteRepository>();
            services.AddScoped<IMedicoRepository, EfMedicoRepository>();
            services.AddScoped<ICitaRepository, EfCitaRepository>();

            return services;
        }
    }
}
