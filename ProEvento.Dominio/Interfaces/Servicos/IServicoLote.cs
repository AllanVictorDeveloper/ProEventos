using ProEvento.Dominio.Models;
using System.Threading.Tasks;

namespace ProEvento.Dominio.Interfaces.Servicos
{
    public interface IServicoLote : IServicoBase<Lote>
    {
        Task<Lote[]> GetLoteByEventoIdAsync(int eventoId);

        Task<Lote> GetLoteByIdsAsync(int eventoId, int loteId);
    }
}