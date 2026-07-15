// CitasApp.Infrastructure/Repositories/EfCitaRepository.cs
// Adapter de salida — implementa ICitaRepository usando EF Core contra Postgres.

using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
using CitasApp.Infrastructure.Data;

namespace CitasApp.Infrastructure.Repositories
{
    public class EfCitaRepository : ICitaRepository
    {
        private readonly CitasDbContext _context;

        public EfCitaRepository(CitasDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Cita> ObtenerTodas() => _context.Citas.ToList();

        public Cita ObtenerPorId(int id) => _context.Citas.Find(id);

        public void Agregar(Cita cita)
        {
            _context.Citas.Add(cita);
            _context.SaveChanges();
        }

        public void Actualizar(Cita cita)
        {
            _context.Citas.Update(cita);
            _context.SaveChanges();
        }

        public void Eliminar(int id)
        {
            var cita = _context.Citas.Find(id);
            if (cita != null)
            {
                _context.Citas.Remove(cita);
                _context.SaveChanges();
            }
        }
    }
}
