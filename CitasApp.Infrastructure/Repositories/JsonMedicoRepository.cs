using System.Text.Json;
using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Infrastructure.Repositories
{
    public class JsonMedicoRepository : IMedicoRepository
    {
        private readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Medico.json");

        private List<Medico> ObtenerTodosInterno()
        {
            if (!File.Exists(_filePath)) return new List<Medico>();
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Medico>>(json) ?? new List<Medico>();
        }

        private void Guardar(List<Medico> items)
        {
            var opciones = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(items, opciones);
            File.WriteAllText(_filePath, json);
        }

        public IEnumerable<Medico> ObtenerTodos() => ObtenerTodosInterno();

        public Medico ObtenerPorId(int id) => ObtenerTodosInterno().FirstOrDefault(m => m.Id == id);

        public void Agregar(Medico medico)
        {
            var items = ObtenerTodosInterno();
            medico.Id = items.Count > 0 ? items.Max(m => m.Id) + 1 : 1;
            items.Add(medico);
            Guardar(items);
        }

        public void Actualizar(Medico medico)
        {
            var items = ObtenerTodosInterno();
            var index = items.FindIndex(m => m.Id == medico.Id);
            if (index != -1)
            {
                items[index] = medico;
                Guardar(items);
            }
        }

        public void Eliminar(int id)
        {
            var items = ObtenerTodosInterno();
            var aEliminar = items.FirstOrDefault(m => m.Id == id);
            if (aEliminar != null)
            {
                items.Remove(aEliminar);
                Guardar(items);
            }
        }
    }
}