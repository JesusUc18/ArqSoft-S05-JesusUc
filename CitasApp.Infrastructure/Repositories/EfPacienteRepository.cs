// CitasApp.Infrastructure/Repositories/EfPacienteRepository.cs
// Adapter de salida — implementa IPacienteRepository usando EF Core contra Postgres.

using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
using CitasApp.Infrastructure.Data;

namespace CitasApp.Infrastructure.Repositories
{
    public class EfPacienteRepository : IPacienteRepository
    {
        private readonly CitasDbContext _context;

        public EfPacienteRepository(CitasDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Paciente> ObtenerTodos() => _context.Pacientes.ToList();

        public Paciente ObtenerPorId(int id) => _context.Pacientes.Find(id);

        public void Agregar(Paciente paciente)
        {
            _context.Pacientes.Add(paciente);
            _context.SaveChanges();
        }

        public void Actualizar(Paciente paciente)
        {
            _context.Pacientes.Update(paciente);
            _context.SaveChanges();
        }

        public void Eliminar(int id)
        {
            var paciente = _context.Pacientes.Find(id);
            if (paciente != null)
            {
                _context.Pacientes.Remove(paciente);
                _context.SaveChanges();
            }
        }
    }
}
