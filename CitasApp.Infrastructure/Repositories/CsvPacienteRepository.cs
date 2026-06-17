// CitasApp.Infrastructure/Repositories/CsvPacienteRepository.cs
// Adapter de salida — implementa IPacienteRepository leyendo/escribiendo un archivo CSV
//
// Adaptado al modelo Paciente real del proyecto (la propiedad se llama "Name", no "Nombre")
// e implementa el ciclo CRUD completo que exige IPacienteRepository
// (ObtenerTodos, ObtenerPorId, Agregar, Actualizar, Eliminar).

using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Infrastructure.Repositories
{
    public class CsvPacienteRepository : IPacienteRepository
    {
        private readonly string _filePath;

        public CsvPacienteRepository(string filePath)
        {
            _filePath = filePath;

            if (!File.Exists(_filePath))
                File.WriteAllText(_filePath, "Id,Nombre,Apellido,Email,Telefono\n");
        }

        // ── Helpers ─────────────────────────────────────────────────────────────

        private List<Paciente> LeerTodos()
        {
            var lista = new List<Paciente>();

            foreach (var linea in File.ReadAllLines(_filePath).Skip(1))
            {
                if (string.IsNullOrWhiteSpace(linea)) continue;
                var p = linea.Split(',');
                if (p.Length < 5) continue;

                lista.Add(new Paciente
                {
                    Id       = int.Parse(p[0]),
                    Name     = p[1],
                    Apellido = p[2],
                    Email    = p[3],
                    Telefono = p[4]
                });
            }

            return lista;
        }

        private void EscribirTodos(List<Paciente> pacientes)
        {
            var lineas = new List<string> { "Id,Nombre,Apellido,Email,Telefono" };

            foreach (var p in pacientes)
            {
                lineas.Add(
                    $"{p.Id}," +
                    $"{Limpiar(p.Name)}," +
                    $"{Limpiar(p.Apellido)}," +
                    $"{Limpiar(p.Email)}," +
                    $"{Limpiar(p.Telefono)}"
                );
            }

            File.WriteAllLines(_filePath, lineas);
        }

        private static string Limpiar(string texto) =>
            (texto ?? string.Empty).Replace(",", ";");

        // ── Port (IPacienteRepository) ─────────────────────────────────────────

        public IEnumerable<Paciente> ObtenerTodos() => LeerTodos();

        public Paciente ObtenerPorId(int id) =>
            LeerTodos().FirstOrDefault(p => p.Id == id);

        public void Agregar(Paciente paciente)
        {
            var pacientes = LeerTodos();
            paciente.Id = pacientes.Count > 0 ? pacientes.Max(p => p.Id) + 1 : 1;
            pacientes.Add(paciente);
            EscribirTodos(pacientes);
        }

        public void Actualizar(Paciente paciente)
        {
            var pacientes = LeerTodos();
            var index = pacientes.FindIndex(p => p.Id == paciente.Id);
            if (index != -1)
            {
                pacientes[index] = paciente;
                EscribirTodos(pacientes);
            }
        }

        public void Eliminar(int id)
        {
            var pacientes = LeerTodos();
            var aEliminar = pacientes.FirstOrDefault(p => p.Id == id);
            if (aEliminar != null)
            {
                pacientes.Remove(aEliminar);
                EscribirTodos(pacientes);
            }
        }
    }
}
