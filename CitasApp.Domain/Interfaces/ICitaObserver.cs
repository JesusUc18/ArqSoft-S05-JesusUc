namespace CitasApp.Domain.Interfaces
{
    public interface ICitaObserver
    {
        void Notificar(Domain.Models.Cita cita);
    }
}