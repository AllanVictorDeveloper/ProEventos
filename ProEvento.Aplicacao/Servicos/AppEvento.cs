using AutoMapper;
using Microsoft.Extensions.Logging;
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

        public async Task<EventoResponse> AdicionarEvento(EventoRequest eventoRequest)
        {
            try
            {
                var evento = _mapper.Map<Evento>(eventoRequest);

                _servicoEvento.Add(evento);

                if (await _servicoEvento.SaveChangesAsync())
                {
                    var eventoRetorno = await _servicoEvento.GetEventoByIdAsync(evento.Id, false);
                    return _mapper.Map<EventoResponse>(eventoRetorno);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoResponse> AtualizarEvento(EventoRequest evento)
        {
            try
            {
                var eventoListado = await _servicoEvento.GetEventoByIdAsync(evento.Id, true);

                var EventoAdd = _mapper.Map<Evento>(evento);

                _servicoEvento.Update(EventoAdd);

                if (await _servicoEvento.SaveChangesAsync())
                {
                    var eventoRetorno = await _servicoEvento.GetEventoByIdAsync(EventoAdd.Id, false);
                    return _mapper.Map<EventoResponse>(eventoRetorno);
                }

                return null;
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