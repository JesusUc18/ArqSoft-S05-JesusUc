using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Application.Services
{
    public class CitaService
    {
        private readonly ICitaRepository _repository;

        public CitaService(ICitaRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Cita> ObtenerTodos() => _repository.ObtenerTodas();

        public Cita? ObtenerPorId(int id) => _repository.ObtenerPorId(id);

        public List<Cita> ObtenerPorPaciente(int pacienteId) =>
            _repository.ObtenerTodas()
                       .Where(c => c.PacienteId == pacienteId)
                       .ToList();

        public void Agregar(Cita cita) => _repository.Agregar(cita);

        public void Actualizar(Cita cita) => _repository.Actualizar(cita);

        public void Eliminar(int id) => _repository.Eliminar(id);
    }
}
