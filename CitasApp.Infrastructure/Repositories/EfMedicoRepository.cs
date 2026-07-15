// CitasApp.Infrastructure/Repositories/EfMedicoRepository.cs
// Adapter de salida — implementa IMedicoRepository usando EF Core contra Postgres.

using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
using CitasApp.Infrastructure.Data;

namespace CitasApp.Infrastructure.Repositories
{
    public class EfMedicoRepository : IMedicoRepository
    {
        private readonly CitasDbContext _context;

        public EfMedicoRepository(CitasDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Medico> ObtenerTodos() => _context.Medicos.ToList();

        public Medico ObtenerPorId(int id) => _context.Medicos.Find(id);

        public void Agregar(Medico medico)
        {
            _context.Medicos.Add(medico);
            _context.SaveChanges();
        }

        public void Actualizar(Medico medico)
        {
            _context.Medicos.Update(medico);
            _context.SaveChanges();
        }

        public void Eliminar(int id)
        {
            var medico = _context.Medicos.Find(id);
            if (medico != null)
            {
                _context.Medicos.Remove(medico);
                _context.SaveChanges();
            }
        }
    }
}
