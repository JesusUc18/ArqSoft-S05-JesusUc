using CitasApp.Domain.Interfaces;
using CitasApp.Infrastructure.Repositories;
using CitasApp.Infrastructure.Observers;
using CitasApp.Application.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// IPacienteRepository: Factory + Decorator de logging
builder.Services.AddScoped<IPacienteRepository>(sp =>
{
    var env = sp.GetRequiredService<IWebHostEnvironment>();
    var repo = RepositoryFactory.CrearPacienteRepository(builder.Environment.EnvironmentName, env);
    return new LoggingPacienteRepository(repo);
});

// IMedicoRepository: Factory + Decorator de logging
builder.Services.AddScoped<IMedicoRepository>(sp =>
{
    var env = sp.GetRequiredService<IWebHostEnvironment>();
    var repo = RepositoryFactory.CrearMedicoRepository(builder.Environment.EnvironmentName, env);
    return new LoggingMedicoRepository(repo);
});

// ICitaRepository: Factory + Decorator de logging
builder.Services.AddScoped<ICitaRepository>(sp =>
{
    var env = sp.GetRequiredService<IWebHostEnvironment>();
    var repo = RepositoryFactory.CrearCitaRepository(builder.Environment.EnvironmentName, env);
    return new LoggingCitaRepository(repo);
});

// Servicios de aplicación
builder.Services.AddScoped<PacienteService>();
builder.Services.AddScoped<MedicoService>();
builder.Services.AddScoped<CitaService>();

// Observers de Cita (antes se creaban con "new" dentro de CitasController —
// Tight Coupling). Ahora se registran aquí y CitaService los recibe
// inyectados como IEnumerable<ICitaObserver>.
builder.Services.AddScoped<ICitaObserver, EmailObserver>();
builder.Services.AddScoped<ICitaObserver, SmsObserver>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();