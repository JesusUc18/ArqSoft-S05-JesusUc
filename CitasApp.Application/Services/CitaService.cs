using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Application.Services
{
    public class CitaService
    {
        private readonly ICitaRepository _repository;
        private readonly IEnumerable<ICitaObserver> _observers;

        // Antes: CitaService no recibía observers, y quien llamaba a Confirmar()
        // (CitasController, en la capa Api) decidía con "new SmsObserver()" /
        // "new EmailObserver()" quién se notificaba (Tight Coupling).
        // Ahora: los observers se inyectan por constructor. CitaService ya no
        // depende de clases concretas de Infrastructure, solo de la interfaz
        // ICitaObserver. Quién se registra se decide en Program.cs (DI).
        public CitaService(ICitaRepository repository, IEnumerable<ICitaObserver> observers)
        {
            _repository = repository;
            _observers = observers;
        }

        public void Confirmar(int id)
        {
            var cita = _repository.ObtenerPorId(id);
            if (cita == null) return;
            cita.Estado = "Confirmada";
            _repository.Actualizar(cita);
            foreach (var obs in _observers)
                obs.Notificar(cita);
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