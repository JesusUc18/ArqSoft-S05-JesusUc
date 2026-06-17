// CitasApp.Infrastructure/Repositories/SqliteMedicoRepository.cs
// Adapter de salida — implementa IMedicoRepository usando SQLite
//
// ANTES DE USAR:
//   En CitasApp.Infrastructure ejecuta:
//   dotnet add package Microsoft.Data.Sqlite
//
// Adaptado al modelo Medico real del proyecto (propiedad "Name", no "Nombre")
// y se agregaron Agregar/Actualizar/Eliminar para cumplir IMedicoRepository
// (el archivo original solo traía ObtenerTodos y ObtenerPorId).

using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
using Microsoft.Data.Sqlite;

namespace CitasApp.Infrastructure.Repositories
{
    public class SqliteMedicoRepository : IMedicoRepository
    {
        private readonly string _connectionString;

        public SqliteMedicoRepository(string dbPath)
        {
            _connectionString = $"Data Source={dbPath}";
            InicializarTabla();
        }

        private void InicializarTabla()
        {
            using var conn = Conectar();
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Medicos (
                    Id             INTEGER PRIMARY KEY AUTOINCREMENT,
                    Nombre         TEXT NOT NULL,
                    Apellido       TEXT NOT NULL,
                    Especialidad   TEXT,
                    NumeroLicencia TEXT
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

        private static Medico LeerFila(SqliteDataReader r) => new Medico
        {
            Id             = r.GetInt32(0),
            Name           = r.GetString(1),
            Apellido       = r.GetString(2),
            Especialidad   = r.IsDBNull(3) ? string.Empty : r.GetString(3),
            NumeroLicencia = r.IsDBNull(4) ? string.Empty : r.GetString(4)
        };

        // ── Port (IMedicoRepository) ────────────────────────────────────────────

        public IEnumerable<Medico> ObtenerTodos()
        {
            using var conn = Conectar();
            var cmd = conn.CreateCommand();
            cmd.CommandText =
                "SELECT Id, Nombre, Apellido, Especialidad, NumeroLicencia FROM Medicos;";

            var lista = new List<Medico>();
            using var r = cmd.ExecuteReader();
            while (r.Read()) lista.Add(LeerFila(r));
            return lista;
        }

        public Medico ObtenerPorId(int id)
        {
            using var conn = Conectar();
            var cmd = conn.CreateCommand();
            cmd.CommandText =
                "SELECT Id, Nombre, Apellido, Especialidad, NumeroLicencia " +
                "FROM Medicos WHERE Id = $id;";
            cmd.Parameters.AddWithValue("$id", id);

            using var r = cmd.ExecuteReader();
            return r.Read() ? LeerFila(r) : null;
        }

        public void Agregar(Medico medico)
        {
            using var conn = Conectar();

            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO Medicos (Nombre, Apellido, Especialidad, NumeroLicencia)
                VALUES ($nombre, $apellido, $especialidad, $licencia);";
            cmd.Parameters.AddWithValue("$nombre",       medico.Name ?? string.Empty);
            cmd.Parameters.AddWithValue("$apellido",     medico.Apellido ?? string.Empty);
            cmd.Parameters.AddWithValue("$especialidad", medico.Especialidad ?? string.Empty);
            cmd.Parameters.AddWithValue("$licencia",     medico.NumeroLicencia ?? string.Empty);
            cmd.ExecuteNonQuery();

            var cmdId = conn.CreateCommand();
            cmdId.CommandText = "SELECT last_insert_rowid();";
            medico.Id = Convert.ToInt32(cmdId.ExecuteScalar());
        }

        public void Actualizar(Medico medico)
        {
            using var conn = Conectar();
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                UPDATE Medicos
                SET Nombre = $nombre, Apellido = $apellido,
                    Especialidad = $especialidad, NumeroLicencia = $licencia
                WHERE Id = $id;";
            cmd.Parameters.AddWithValue("$nombre",       medico.Name ?? string.Empty);
            cmd.Parameters.AddWithValue("$apellido",     medico.Apellido ?? string.Empty);
            cmd.Parameters.AddWithValue("$especialidad", medico.Especialidad ?? string.Empty);
            cmd.Parameters.AddWithValue("$licencia",     medico.NumeroLicencia ?? string.Empty);
            cmd.Parameters.AddWithValue("$id",           medico.Id);
            cmd.ExecuteNonQuery();
        }

        public void Eliminar(int id)
        {
            using var conn = Conectar();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM Medicos WHERE Id = $id;";
            cmd.Parameters.AddWithValue("$id", id);
            cmd.ExecuteNonQuery();
        }
    }
}
