using System.Text.Json;
using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Infrastructure.Repositories
{
    public class JsonCitaRepository : ICitaRepository
    {
        private readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Cita.json");

        private List<Cita> ObtenerTodosInterno()
        {
            if (!File.Exists(_filePath)) return new List<Cita>();
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Cita>>(json) ?? new List<Cita>();
        }

        private void Guardar(List<Cita> items)
        {
            var opciones = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(items, opciones);
            File.WriteAllText(_filePath, json);
        }

        public IEnumerable<Cita> ObtenerTodas() => ObtenerTodosInterno();

        public Cita ObtenerPorId(int id) => ObtenerTodosInterno().FirstOrDefault(c => c.Id == id);

        public void Agregar(Cita cita)
        {
            var items = ObtenerTodosInterno();
            cita.Id = items.Count > 0 ? items.Max(c => c.Id) + 1 : 1;
            items.Add(cita);
            Guardar(items);
        }

        public void Actualizar(Cita cita)
        {
            var items = ObtenerTodosInterno();
            var index = items.FindIndex(c => c.Id == cita.Id);
            if (index != -1)
            {
                items[index] = cita;
                Guardar(items);
            }
        }

        public void Eliminar(int id)
        {
            var items = ObtenerTodosInterno();
            var aEliminar = items.FirstOrDefault(c => c.Id == id);
            if (aEliminar != null)
            {
                items.Remove(aEliminar);
                Guardar(items);
            }
        }
    }
}