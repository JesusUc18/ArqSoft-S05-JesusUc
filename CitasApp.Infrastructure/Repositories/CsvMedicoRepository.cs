// CitasApp.Infrastructure/Repositories/CsvMedicoRepository.cs
// Adapter de salida — implementa IMedicoRepository leyendo/escribiendo un archivo CSV
//
// Adaptado al modelo Medico real del proyecto (la propiedad se llama "Name", no "Nombre")
// e implementa el ciclo CRUD completo que exige IMedicoRepository
// (ObtenerTodos, ObtenerPorId, Agregar, Actualizar, Eliminar).

using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Infrastructure.Repositories
{
    public class CsvMedicoRepository : IMedicoRepository
    {
        private readonly string _filePath;

        public CsvMedicoRepository(string filePath)
        {
            _filePath = filePath;

            if (!File.Exists(_filePath))
                File.WriteAllText(_filePath, "Id,Nombre,Apellido,Especialidad,NumeroLicencia\n");
        }

        // ── Helpers ─────────────────────────────────────────────────────────────

        private List<Medico> LeerTodos()
        {
            var lista = new List<Medico>();

            foreach (var linea in File.ReadAllLines(_filePath).Skip(1))
            {
                if (string.IsNullOrWhiteSpace(linea)) continue;
                var p = linea.Split(',');
                if (p.Length < 5) continue;

                lista.Add(new Medico
                {
                    Id             = int.Parse(p[0]),
                    Name           = p[1],
                    Apellido       = p[2],
                    Especialidad   = p[3],
                    NumeroLicencia = p[4]
                });
            }

            return lista;
        }

        private void EscribirTodos(List<Medico> medicos)
        {
            var lineas = new List<string> { "Id,Nombre,Apellido,Especialidad,NumeroLicencia" };

            foreach (var m in medicos)
            {
                lineas.Add(
                    $"{m.Id}," +
                    $"{Limpiar(m.Name)}," +
                    $"{Limpiar(m.Apellido)}," +
                    $"{Limpiar(m.Especialidad)}," +
                    $"{Limpiar(m.NumeroLicencia)}"
                );
            }

            File.WriteAllLines(_filePath, lineas);
        }

        private static string Limpiar(string texto) =>
            (texto ?? string.Empty).Replace(",", ";");

        // ── Port (IMedicoRepository) ────────────────────────────────────────────

        public IEnumerable<Medico> ObtenerTodos() => LeerTodos();

        public Medico ObtenerPorId(int id) =>
            LeerTodos().FirstOrDefault(m => m.Id == id);

        public void Agregar(Medico medico)
        {
            var medicos = LeerTodos();
            medico.Id = medicos.Count > 0 ? medicos.Max(m => m.Id) + 1 : 1;
            medicos.Add(medico);
            EscribirTodos(medicos);
        }

        public void Actualizar(Medico medico)
        {
            var medicos = LeerTodos();
            var index = medicos.FindIndex(m => m.Id == medico.Id);
            if (index != -1)
            {
                medicos[index] = medico;
                EscribirTodos(medicos);
            }
        }

        public void Eliminar(int id)
        {
            var medicos = LeerTodos();
            var aEliminar = medicos.FirstOrDefault(m => m.Id == id);
            if (aEliminar != null)
            {
                medicos.Remove(aEliminar);
                EscribirTodos(medicos);
            }
        }
    }
}
