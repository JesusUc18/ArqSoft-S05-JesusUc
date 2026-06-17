using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Application.Services
{
    public class MedicoService
    {
        private readonly IMedicoRepository _repository;

        public MedicoService(IMedicoRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Medico> ObtenerTodos() => _repository.ObtenerTodos();

        public Medico? ObtenerPorId(int id) => _repository.ObtenerPorId(id);

        public void Agregar(Medico medico) => _repository.Agregar(medico);

        public void Actualizar(Medico medico) => _repository.Actualizar(medico);

        public void Eliminar(int id) => _repository.Eliminar(id);
    }
}
