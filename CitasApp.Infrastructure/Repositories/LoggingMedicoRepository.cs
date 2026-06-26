using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Infrastructure.Repositories
{
    public class LoggingMedicoRepository : IMedicoRepository
    {
        private readonly IMedicoRepository _inner;

        public LoggingMedicoRepository(IMedicoRepository inner) => _inner = inner;

        public IEnumerable<Medico> ObtenerTodos()
        {
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Medico.ObtenerTodos — inicio");
            var resultado = _inner.ObtenerTodos().ToList();
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Medico.ObtenerTodos — {resultado.Count} registros");
            return resultado;
        }

        public Medico ObtenerPorId(int id)
        {
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Medico.ObtenerPorId({id}) — inicio");
            var resultado = _inner.ObtenerPorId(id);
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Medico.ObtenerPorId({id}) — {(resultado != null ? "encontrado" : "no encontrado")}");
            return resultado;
        }

        public void Agregar(Medico medico)
        {
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Medico.Agregar — inicio");
            _inner.Agregar(medico);
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Medico.Agregar — completado (Id: {medico.Id})");
        }

        public void Actualizar(Medico medico)
        {
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Medico.Actualizar({medico.Id}) — inicio");
            _inner.Actualizar(medico);
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Medico.Actualizar({medico.Id}) — completado");
        }

        public void Eliminar(int id)
        {
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Medico.Eliminar({id}) — inicio");
            _inner.Eliminar(id);
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Medico.Eliminar({id}) — completado");
        }
    }
}