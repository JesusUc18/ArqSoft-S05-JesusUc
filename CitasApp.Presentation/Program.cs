using CitasApp.Data;
using CitasApp.Domain.Interfaces;
using CitasApp.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// ─────────────────────────────────────────────────────────────────────────────
// Adapters de Pacientes/Medicos/Citas (Arquitectura Hexagonal)
// AQUÍ se enchufa el Adapter que quieres usar para cada entidad.
// Domain (los Ports/interfaces) NO se toca — solo cambia este bloque.
// Elige UN bloque (A, B o C) y deja los otros comentados.
// ─────────────────────────────────────────────────────────────────────────────

var dataFolder = Path.Combine(Directory.GetCurrentDirectory(), "Data");
Directory.CreateDirectory(dataFolder);

// Rutas para CSV
var csvPacientes = Path.Combine(dataFolder, "pacientes.csv");
var csvMedicos = Path.Combine(dataFolder, "medicos.csv");
var csvCitas = Path.Combine(dataFolder, "citas.csv");

// Ruta para SQLite (un solo archivo .db para las 3 tablas)
var sqlitePath = Path.Combine(dataFolder, "citasapp.db");

// ▶ Bloque A — JSON (como ya funcionaba antes)  ← activo ahora
//builder.Services.AddSingleton<IPacienteRepository, JsonPacienteRepository>();
//builder.Services.AddSingleton<IMedicoRepository, JsonMedicoRepository>();
//builder.Services.AddSingleton<ICitaRepository, JsonCitaRepository>();

// ▶ Bloque B — CSV
builder.Services.AddSingleton<IPacienteRepository>(_ => new CsvPacienteRepository(csvPacientes));
builder.Services.AddSingleton<IMedicoRepository>  (_ => new CsvMedicoRepository(csvMedicos));
builder.Services.AddSingleton<ICitaRepository>    (_ => new CsvCitaRepository(csvCitas));

// ▶ Bloque C — SQLite (requiere el paquete Microsoft.Data.Sqlite en CitasApp.Infrastructure)
//builder.Services.AddSingleton<IPacienteRepository>(_ => new SqlitePacienteRepository(sqlitePath));
//builder.Services.AddSingleton<IMedicoRepository>  (_ => new SqliteMedicoRepository(sqlitePath));
//builder.Services.AddSingleton<ICitaRepository>    (_ => new SqliteCitaRepository(sqlitePath));

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
