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
            this._servicoEvento = servicoEvento;
            _mapper = mapper;
        }

        public async Task<EventoResponse> AdicionarEvento(EventoRequest eventoRequest)
        {
            try
            {
                var evento = _mapper.Map<Evento>(eventoRequest);

                var eventoAdicionado = _servicoEvento.Add(evento);

                //var eventoRetorno = await _servicoEvento.GetEventoByIdAsync(evento.Id, false);

                var eventoResponse = _mapper.Map<EventoResponse>(eventoAdicionado);

                return eventoResponse;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoResponse> AtualizarEvento(int id, EventoRequest evento)
        {
            try
            {
                var eventoListado = _servicoEvento.GetEventoByIdAsync(id, true);

                eventoListado.Id = id;
                eventoListado.Local = evento.Local;
                eventoListado.DataEvento = evento.DataEvento;
                eventoListado.Email = evento.Email;
                eventoListado.ImagemUrl = evento.ImagemUrl;
                eventoListado.QtdPessoas = evento.QtdPessoas;
                eventoListado.Tema = evento.Tema;
                eventoListado.Telefone = evento.Telefone;

                var eventoAdd = _servicoEvento.Update(eventoListado);

                if (eventoAdd != null)
                {
                    var eventoRetorno = _servicoEvento.GetEventoByIdAsync(eventoAdd.Id, false);
                    var eventoMapper = _mapper.Map<EventoResponse>(eventoRetorno);
                    return eventoMapper;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> DeletarEvento(int id)
        {
            try
            {
                var eventoListado = _servicoEvento.GetEventoByIdAsync(id, false);

                if (eventoListado != null)
                {
                    _servicoEvento.Delete(eventoListado);
                    return "Deletado";
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
                var evento = _servicoEvento.GetEventoByIdAsync(eventoId, includePalestrantes);
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