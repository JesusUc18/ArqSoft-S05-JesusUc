using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Application.Services
{
    public class PacienteService
    {
        private readonly IPacienteRepository _repository;

        public PacienteService(IPacienteRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Paciente> ObtenerTodos() => _repository.ObtenerTodos();

        public Paciente? ObtenerPorId(int id) => _repository.ObtenerPorId(id);

        public void Agregar(Paciente paciente) => _repository.Agregar(paciente);

        public void Actualizar(Paciente paciente) => _repository.Actualizar(paciente);

        public void Eliminar(int id) => _repository.Eliminar(id);
    }
}
