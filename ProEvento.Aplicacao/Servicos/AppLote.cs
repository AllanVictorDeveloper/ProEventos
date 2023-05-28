using AutoMapper;
using ProEvento.Aplicacao.Dto;
using ProEvento.Aplicacao.Interfaces.Servicos;
using ProEvento.Dominio.Interfaces.Repositorios;
using ProEvento.Dominio.Interfaces.Servicos;
using System;
using System.Threading.Tasks;

namespace ProEvento.Aplicacao.Servicos
{
    public class AppLote : IAppLote
    {
        private readonly IServicoLote _servicoLote;
        private readonly IRepositorioLote _repositorioLote;

        private readonly IMapper _mapper;

        public AppLote(IServicoLote servicoLote, IMapper mapper, IRepositorioLote repositorioLote)
        {
            _servicoLote = servicoLote;
            _mapper = mapper;
            _repositorioLote = repositorioLote;
        }

        public Task<LoteResponse> SaveLote(int eventoId, LoteResponse[] lotes)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> DeletarLote(int eventoId, int loteId)
        {
            try
            {
                var lote = _servicoLote.GetLoteByIdsAsync(eventoId, loteId);

                if (lote == null)
                    throw new Exception("Lote para delete não encontrado");

                _repositorioLote.Delete(await lote);
                return await _repositorioLote.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LoteResponse[]> GetLoteByEventoIdAsync(int eventoId)
        {
            try
            {
                var lotes = await _servicoLote.GetLoteByEventoIdAsync(eventoId);

                if (lotes == null)
                    return null;

                var lotesResponse = _mapper.Map<LoteResponse[]>(lotes);

                return lotesResponse;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LoteResponse> GetLoteByIdsAsync(int eventoId, int loteId)
        {
            try
            {
                var lote = await _servicoLote.GetLoteByIdsAsync(eventoId, loteId);

                if (lote == null)
                    return null;

                var lotesResponse = _mapper.Map<LoteResponse>(lote);

                return lotesResponse;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}