using CitasApp.Domain.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace CitasApp.Infrastructure.Repositories
{
    // FACTORY — centraliza la decisión de qué repositorio instanciar (SRP de SOLID)
    public static class RepositoryFactory
    {
        public static IPacienteRepository CrearPacienteRepository(
            string entorno, IWebHostEnvironment env)
        {
            return entorno switch
            {
                "Production" => new MemoriaPacienteRepository(),   // simula SQL
                _ => new JsonPacienteRepository()        // JSON para dev/test
            };
        }

        public static IMedicoRepository CrearMedicoRepository(
            string entorno, IWebHostEnvironment env)
        {
            return entorno switch
            {
                "Production" => new JsonMedicoRepository(),
                _ => new JsonMedicoRepository()
            };
        }

        public static ICitaRepository CrearCitaRepository(
            string entorno, IWebHostEnvironment env)
        {
            return entorno switch
            {
                "Production" => new JsonCitaRepository(),
                _ => new JsonCitaRepository()
            };
        }
    }
}
