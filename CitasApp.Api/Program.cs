using CitasApp.Domain.Interfaces;
using CitasApp.Infrastructure.Repositories;
using CitasApp.Application.Services;
using CitasApp.Infrastructure.Calculo;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Repositorios
builder.Services.AddScoped<IPacienteRepository, JsonPacienteRepository>();
builder.Services.AddScoped <IMedicoRepository, JsonMedicoRepository>();
builder.Services.AddScoped<ICitaRepository, JsonCitaRepository>();
builder.Services.AddScoped<ICalculadora, CalculadoraBasica>();

// Servicios de aplicación
builder.Services.AddScoped<PacienteService>();
builder.Services.AddScoped<MedicoService>();
builder.Services.AddScoped<CitaService>();
builder.Services.AddScoped<CalculadoraService>();

var app = builder.Build();

app.UseDefaultFiles();     // nuevo: hace que "/" cargue index.html
app.UseStaticFiles();      // nuevo: sirve archivos de wwwroot
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
