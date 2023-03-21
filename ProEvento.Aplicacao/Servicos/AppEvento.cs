using AutoMapper;
using ProEvento.Aplicacao.Dto;
using ProEvento.Aplicacao.Interfaces.Servicos;
using ProEvento.Dominio.Interfaces.Servicos;
using ProEventos.Domain.Models;
using System;
using System.Threading.Tasks;

namespace ProEvento.Aplicacao.Servicos
{
    public class AppEvento : AppBase<Evento>, IAppEvento
    {
        private readonly IServicoEvento _servicoEvento;
        private readonly IMapper _mapper;

        public AppEvento(IServicoEvento servicoEvento, IMapper mapper) : base(servicoEvento)
        {
            _servicoEvento = servicoEvento;
            _mapper = mapper;
        }

        public async Task<EventoResponse> AtualizarEvento(EventoRequest evento, int id)
        {
            try
            {
                var eventoListado = await _servicoEvento.GetEventoByIdAsync(id, true);

                var eventoRetorno = _mapper.Map<EventoResponse>(eventoListado);

                var EventoAdd = _mapper.Map<Evento>(evento);

                if (eventoRetorno == null)
                    return null;

                _servicoEvento.Update(EventoAdd);

                return eventoRetorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoResponse[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _servicoEvento.GetAllEventosAsync(includePalestrantes);
                var eventosRetorno = _mapper.Map<EventoResponse[]>(eventos);

                if (eventosRetorno == null) return null;

                return eventosRetorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoResponse[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _servicoEvento.GetAllEventosByTemaAsync(tema, includePalestrantes);
                var eventosRetorno = _mapper.Map<EventoResponse[]>(eventos);

                if (eventosRetorno == null) return null;

                return eventosRetorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoResponse> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            try
            {
                var evento = await _servicoEvento.GetEventoByIdAsync(eventoId, includePalestrantes);
                var eventoRetorno = _mapper.Map<EventoResponse>(evento);

                if (eventoRetorno == null) return null;

                return eventoRetorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}