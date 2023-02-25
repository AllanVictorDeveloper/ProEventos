using Microsoft.EntityFrameworkCore;
using ProEvento.Dominio.Interfaces.Repositorios;
using ProEventos.Api.Data;
using ProEventos.Domain.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ProEvento.Infraestrutura.Repositorio
{
    public class RepositorioEventos : RepositorioBase<Evento>, IRepositorioEventos
    {
        public RepositorioEventos(ProEventoContext proEventoContext) : base(proEventoContext)
        {
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _proEventoContext.Eventos
                .Include(e => e.Lotes)
                .Include(e => e.RedesSociais)
                .OrderBy(e => e.Id);

            if (includePalestrantes)
            {
                query.Include(e => e.PalestrantesEventos)
                    .ThenInclude(pe => pe.Palestrante);
            }

            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _proEventoContext.Eventos
                .Include(e => e.Lotes)
                .Include(e => e.RedesSociais)
                .OrderBy(e => e.Id)
                .Where(e => e.Tema.ToLower().Contains(tema.ToLower()));

            if (includePalestrantes)
            {
                query.Include(e => e.PalestrantesEventos)
                    .ThenInclude(pe => pe.Palestrante);
            }

            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _proEventoContext.Eventos
                .Include(e => e.Lotes)
                .Include(e => e.RedesSociais)
                .OrderBy(e => e.Id)
                .Where(e => e.Id == eventoId);

            if (includePalestrantes)
            {
                query.Include(e => e.PalestrantesEventos)
                    .ThenInclude(pe => pe.Palestrante);
            }

            return await query.FirstOrDefaultAsync();
        }
    }
}