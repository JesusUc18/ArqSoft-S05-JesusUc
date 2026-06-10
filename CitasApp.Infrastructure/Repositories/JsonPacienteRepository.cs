using System.Text.Json;
using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Infrastructure.Repositories
{
    public class JsonPacienteRepository : IPacienteRepository
    {
        private readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pacientes.json");

        private List<Paciente> ObtenerTodosInterno()
        {
            if (!File.Exists(_filePath)) return new List<Paciente>();
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Paciente>>(json) ?? new List<Paciente>();
        }

        private void Guardar(List<Paciente> items)
        {
            var opciones = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(items, opciones);
            File.WriteAllText(_filePath, json);
        }

        public IEnumerable<Paciente> ObtenerTodos() => ObtenerTodosInterno();

        public Paciente ObtenerPorId(int id) => ObtenerTodosInterno().FirstOrDefault(p => p.Id == id);

        public void Agregar(Paciente paciente)
        {
            var items = ObtenerTodosInterno();
            paciente.Id = items.Count > 0 ? items.Max(p => p.Id) + 1 : 1;
            items.Add(paciente);
            Guardar(items);
        }

        public void Actualizar(Paciente paciente)
        {
            var items = ObtenerTodosInterno();
            var index = items.FindIndex(p => p.Id == paciente.Id);
            if (index != -1)
            {
                items[index] = paciente;
                Guardar(items);
            }
        }

        public void Eliminar(int id)
        {
            var items = ObtenerTodosInterno();
            var aEliminar = items.FirstOrDefault(p => p.Id == id);
            if (aEliminar != null)
            {
                items.Remove(aEliminar);
                Guardar(items);
            }
        }
    }
}