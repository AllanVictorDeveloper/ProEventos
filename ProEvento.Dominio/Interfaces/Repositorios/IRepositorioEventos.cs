using ProEventos.Domain.Models;
using System.Threading.Tasks;

namespace ProEvento.Dominio.Interfaces.Repositorios
{
    public interface IRepositorioEventos : IRepositorioBase<Evento>
    {
        Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes);

        Task<Evento[]> GetAllEventosAsync(bool includePalestrantes);

        Evento GetEventoByIdAsync(int eventoId, bool includePalestrantes);
    }
}