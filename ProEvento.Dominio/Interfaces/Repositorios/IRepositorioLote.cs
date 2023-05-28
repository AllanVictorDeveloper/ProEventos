using ProEvento.Dominio.Models;
using System.Threading.Tasks;

namespace ProEvento.Dominio.Interfaces.Repositorios
{
    public interface IRepositorioLote : IRepositorioBase<Lote>
    {
        Task<Lote[]> GetLosteByEventoIdAsync(int eventoId);

        Task<Lote> GetLoteByIdsAsync(int eventoId, int loteId);
    }
}