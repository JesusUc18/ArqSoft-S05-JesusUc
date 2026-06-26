using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Infrastructure.Repositories
{
    // Simula un repositorio SQL en memoria (usado en entorno Production)
    public class MemoriaPacienteRepository : IPacienteRepository
    {
        private static readonly List<Paciente> _datos = new()
        {
            new Paciente { Id = 1, Name = "Carlos",  Apellido = "Ramírez", Email = "carlos@mail.com",  Telefono = "555-0001" },
            new Paciente { Id = 2, Name = "Laura",   Apellido = "Torres",  Email = "laura@mail.com",   Telefono = "555-0002" },
            new Paciente { Id = 3, Name = "Miguel",  Apellido = "López",   Email = "miguel@mail.com",  Telefono = "555-0003" },
        };

        public IEnumerable<Paciente> ObtenerTodos() => _datos.ToList();

        public Paciente ObtenerPorId(int id) => _datos.FirstOrDefault(p => p.Id == id);

        public void Agregar(Paciente paciente)
        {
            paciente.Id = _datos.Count > 0 ? _datos.Max(p => p.Id) + 1 : 1;
            _datos.Add(paciente);
        }

        public void Actualizar(Paciente paciente)
        {
            var index = _datos.FindIndex(p => p.Id == paciente.Id);
            if (index != -1) _datos[index] = paciente;
        }

        public void Eliminar(int id)
        {
            var item = _datos.FirstOrDefault(p => p.Id == id);
            if (item != null) _datos.Remove(item);
        }
    }
}
