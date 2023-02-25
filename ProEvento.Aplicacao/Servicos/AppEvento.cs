using Microsoft.Extensions.Logging;
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

        public AppEvento(IServicoEvento servicoEvento) : base(servicoEvento)
        {
            _servicoEvento = servicoEvento;
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _servicoEvento.GetAllEventosAsync(includePalestrantes);
                if (eventos == null) return null;
                return eventos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _servicoEvento.GetAllEventosByTemaAsync(tema, includePalestrantes);
                if (eventos == null) return null;
                return eventos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            try
            {
                var evento = await _servicoEvento.GetEventoByIdAsync(eventoId, includePalestrantes);
                if (evento == null) return null;
                return evento;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}