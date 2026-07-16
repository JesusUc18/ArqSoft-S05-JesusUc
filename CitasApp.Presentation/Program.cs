using CitasApp.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Antes: este archivo tenía ~92 líneas mezclando DbContexts, Identity,
// selección de repositorios (con bloques comentados) y pipeline HTTP —
// un God File sin una sola razón para cambiar (5 responsabilidades distintas
// en el mismo lugar).
// Ahora: cada responsabilidad de arranque vive en su propio extension method
// bajo Extensions/. Program.cs solo orquesta.
builder.Services
    .AddPersistence(builder.Configuration)
    .AddIdentityConfig()
    .AddDomainRepositories();

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