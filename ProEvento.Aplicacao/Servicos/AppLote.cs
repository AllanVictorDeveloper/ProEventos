using AutoMapper;
using ProEvento.Aplicacao.Dto;
using ProEvento.Aplicacao.Interfaces.Servicos;
using ProEvento.Dominio.Interfaces.Repositorios;
using ProEvento.Dominio.Interfaces.Servicos;
using ProEvento.Dominio.Models;
using System;
using System.Linq;
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

        public async Task AddLote(int eventoId, LoteRequest models)
        {
            try
            {
                var lote = _mapper.Map<Lote>(models);

                lote.EventoId = eventoId;

                _servicoLote.Add(lote);

                _servicoLote.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LoteResponse[]> SaveLote(int eventoId, LoteRequest[] models)
        {
            try
            {
                var lotes = await _servicoLote.GetLoteByEventoIdAsync(eventoId);
                if (lotes == null)
                    return null;

                foreach (var model in models)
                {
                    if (model.Id == 0)
                    {
                        await AddLote(eventoId, model);
                    }
                    else
                    {
                        var lote = lotes.FirstOrDefault(lote => lote.Id == model.Id);

                        model.EventoId = eventoId;

                        _mapper.Map(model, lote);

                        _servicoLote.Update(lote);

                        _repositorioLote.SaveChanges();
                    }
                }

                var loteRetorno = await _servicoLote.GetLoteByEventoIdAsync(eventoId);

                return _mapper.Map<LoteResponse[]>(loteRetorno);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeletarLote(int eventoId, int loteId)
        {
            try
            {
                var lote = _servicoLote.GetLoteByIdsAsync(eventoId, loteId);

                if (lote == null)
                    throw new Exception("Lote para delete não encontrado");

                var retorno = _repositorioLote.Delete(await lote);

                if (retorno == null)
                    return false;

                return true;
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

                if (lotes.Length == 0)
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