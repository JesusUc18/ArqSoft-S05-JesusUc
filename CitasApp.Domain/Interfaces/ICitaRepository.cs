using CitasApp.Domain.Models;

namespace CitasApp.Domain.Interfaces
{
    public interface ICitaRepository
    {
        IEnumerable<Cita> ObtenerTodas();
        Cita ObtenerPorId(int id);
        void Agregar(Cita cita);
        void Actualizar(Cita cita);
        void Eliminar(int id);
    }
}