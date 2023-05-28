using ProEvento.Dominio.Interfaces.Repositorios;
using ProEvento.Dominio.Interfaces.Servicos;
using ProEvento.Dominio.Models;
using System.Threading.Tasks;

namespace ProEvento.Dominio.Servicos
{
    public class ServicoLote : ServicoBase<Lote>, IServicoLote
    {
        private readonly IRepositorioLote _repositorioLote;

        public ServicoLote(IRepositorioLote repositorioLote) : base(repositorioLote)
        {
            _repositorioLote = repositorioLote;
        }

        public async Task<Lote[]> GetLoteByEventoIdAsync(int eventoId)
        {
            return await _repositorioLote.GetLosteByEventoIdAsync(eventoId);
        }

        public async Task<Lote> GetLoteByIdsAsync(int eventoId, int loteId)
        {
            return await _repositorioLote.GetLoteByIdsAsync(eventoId, loteId);
        }
    }
}