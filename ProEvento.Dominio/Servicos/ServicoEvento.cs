using ProEvento.Dominio.Interfaces.Repositorios;
using ProEvento.Dominio.Interfaces.Servicos;
using ProEventos.Domain.Models;
using System.Threading.Tasks;

namespace ProEvento.Dominio.Servicos
{
    public class ServicoEvento : ServicoBase<Evento>, IServicoEvento
    {
        private readonly IRepositorioEventos _repositorioEventos;

        public ServicoEvento(IRepositorioEventos repositorioEventos) : base(repositorioEventos)
        {
            _repositorioEventos = repositorioEventos;
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            return await _repositorioEventos.GetAllEventosAsync(includePalestrantes);
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            return await _repositorioEventos.GetAllEventosByTemaAsync(tema, includePalestrantes);
        }

        public Evento GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            var evento = _repositorioEventos.GetEventoByIdAsync(eventoId, includePalestrantes);
            return evento;
        }
    }
}