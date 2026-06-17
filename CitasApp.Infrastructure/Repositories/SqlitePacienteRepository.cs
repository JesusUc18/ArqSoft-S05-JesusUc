// CitasApp.Infrastructure/Repositories/SqlitePacienteRepository.cs
// Adapter de salida — implementa IPacienteRepository usando SQLite
//
// Comparte el mismo archivo .db que SqliteCitaRepository y SqliteMedicoRepository.
// Pasa la misma ruta dbPath desde Program.cs.
//
// ANTES DE USAR:
//   En CitasApp.Infrastructure ejecuta:
//   dotnet add package Microsoft.Data.Sqlite
//
// Adaptado al modelo Paciente real del proyecto (propiedad "Name", no "Nombre")
// y se agregaron Agregar/Actualizar/Eliminar para cumplir IPacienteRepository
// (el archivo original solo traía ObtenerTodos y ObtenerPorId).

using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
using Microsoft.Data.Sqlite;

namespace CitasApp.Infrastructure.Repositories
{
    public class SqlitePacienteRepository : IPacienteRepository
    {
        private readonly string _connectionString;

        public SqlitePacienteRepository(string dbPath)
        {
            _connectionString = $"Data Source={dbPath}";
            InicializarTabla();
        }

        private void InicializarTabla()
        {
            using var conn = Conectar();
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Pacientes (
                    Id       INTEGER PRIMARY KEY AUTOINCREMENT,
                    Nombre   TEXT NOT NULL,
                    Apellido TEXT NOT NULL,
                    Email    TEXT,
                    Telefono TEXT
                );";
            cmd.ExecuteNonQuery();
        }

        // ── Helpers ─────────────────────────────────────────────────────────────

        private SqliteConnection Conectar()
        {
            var conn = new SqliteConnection(_connectionString);
            conn.Open();
            return conn;
        }

        private static Paciente LeerFila(SqliteDataReader r) => new Paciente
        {
            Id       = r.GetInt32(0),
            Name     = r.GetString(1),
            Apellido = r.GetString(2),
            Email    = r.IsDBNull(3) ? string.Empty : r.GetString(3),
            Telefono = r.IsDBNull(4) ? string.Empty : r.GetString(4)
        };

        // ── Port (IPacienteRepository) ─────────────────────────────────────────

        public IEnumerable<Paciente> ObtenerTodos()
        {
            using var conn = Conectar();
            var cmd = conn.CreateCommand();
            cmd.CommandText =
                "SELECT Id, Nombre, Apellido, Email, Telefono FROM Pacientes;";

            var lista = new List<Paciente>();
            using var r = cmd.ExecuteReader();
            while (r.Read()) lista.Add(LeerFila(r));
            return lista;
        }

        public Paciente ObtenerPorId(int id)
        {
            using var conn = Conectar();
            var cmd = conn.CreateCommand();
            cmd.CommandText =
                "SELECT Id, Nombre, Apellido, Email, Telefono " +
                "FROM Pacientes WHERE Id = $id;";
            cmd.Parameters.AddWithValue("$id", id);

            using var r = cmd.ExecuteReader();
            return r.Read() ? LeerFila(r) : null;
        }

        public void Agregar(Paciente paciente)
        {
            using var conn = Conectar();

            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO Pacientes (Nombre, Apellido, Email, Telefono)
                VALUES ($nombre, $apellido, $email, $telefono);";
            cmd.Parameters.AddWithValue("$nombre",   paciente.Name ?? string.Empty);
            cmd.Parameters.AddWithValue("$apellido", paciente.Apellido ?? string.Empty);
            cmd.Parameters.AddWithValue("$email",    paciente.Email ?? string.Empty);
            cmd.Parameters.AddWithValue("$telefono", paciente.Telefono ?? string.Empty);
            cmd.ExecuteNonQuery();

            var cmdId = conn.CreateCommand();
            cmdId.CommandText = "SELECT last_insert_rowid();";
            paciente.Id = Convert.ToInt32(cmdId.ExecuteScalar());
        }

        public void Actualizar(Paciente paciente)
        {
            using var conn = Conectar();
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                UPDATE Pacientes
                SET Nombre = $nombre, Apellido = $apellido, Email = $email, Telefono = $telefono
                WHERE Id = $id;";
            cmd.Parameters.AddWithValue("$nombre",   paciente.Name ?? string.Empty);
            cmd.Parameters.AddWithValue("$apellido", paciente.Apellido ?? string.Empty);
            cmd.Parameters.AddWithValue("$email",    paciente.Email ?? string.Empty);
            cmd.Parameters.AddWithValue("$telefono", paciente.Telefono ?? string.Empty);
            cmd.Parameters.AddWithValue("$id",       paciente.Id);
            cmd.ExecuteNonQuery();
        }

        public void Eliminar(int id)
        {
            using var conn = Conectar();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM Pacientes WHERE Id = $id;";
            cmd.Parameters.AddWithValue("$id", id);
            cmd.ExecuteNonQuery();
        }
    }
}
