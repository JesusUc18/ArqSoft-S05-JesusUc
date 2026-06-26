using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Infrastructure.Repositories
{
    public class LoggingCitaRepository : ICitaRepository
    {
        private readonly ICitaRepository _inner;

        public LoggingCitaRepository(ICitaRepository inner) => _inner = inner;

        public IEnumerable<Cita> ObtenerTodas()
        {
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Cita.ObtenerTodas — inicio");
            var resultado = _inner.ObtenerTodas().ToList();
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Cita.ObtenerTodas — {resultado.Count} registros");
            return resultado;
        }

        public Cita ObtenerPorId(int id)
        {
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Cita.ObtenerPorId({id}) — inicio");
            var resultado = _inner.ObtenerPorId(id);
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Cita.ObtenerPorId({id}) — {(resultado != null ? "encontrado" : "no encontrado")}");
            return resultado;
        }

        public void Agregar(Cita cita)
        {
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Cita.Agregar — inicio");
            _inner.Agregar(cita);
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Cita.Agregar — completado (Id: {cita.Id})");
        }

        public void Actualizar(Cita cita)
        {
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Cita.Actualizar({cita.Id}) — inicio");
            _inner.Actualizar(cita);
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Cita.Actualizar({cita.Id}) — completado");
        }

        public void Eliminar(int id)
        {
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Cita.Eliminar({id}) — inicio");
            _inner.Eliminar(id);
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Cita.Eliminar({id}) — completado");
        }
    }
}