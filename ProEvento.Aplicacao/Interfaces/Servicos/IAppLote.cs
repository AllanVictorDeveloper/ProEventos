using ProEvento.Aplicacao.Dto;
using System.Threading.Tasks;

namespace ProEvento.Aplicacao.Interfaces.Servicos
{
    public interface IAppLote
    {
        Task<LoteResponse[]> GetLoteByEventoIdAsync(int eventoId);

        Task<LoteResponse> GetLoteByIdsAsync(int eventoId, int loteId);

        Task<LoteResponse> SaveLote(int eventoId, LoteResponse[] lotes);

        Task<bool> DeletarLote(int eventoId, int loteId);
    }
}