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
        Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes);

        Task<Evento[]> GetAllEventosAsync(bool includePalestrantes);

        Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes);

    }
}
