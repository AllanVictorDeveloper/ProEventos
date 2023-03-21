using ProEvento.Aplicacao.Dto;
using ProEventos.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEvento.Aplicacao.Interfaces.Servicos
{
    public interface IAppEvento : IAppBase<Evento>
    {
        Task<EventoResponse[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes);

        Task<EventoResponse[]> GetAllEventosAsync(bool includePalestrantes);

        Task<EventoResponse> GetEventoByIdAsync(int eventoId, bool includePalestrantes);

        Task<EventoResponse> AtualizarEvento(EventoRequest evento, int id);

    }
}
