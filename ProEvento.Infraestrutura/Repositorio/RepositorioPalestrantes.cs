using Microsoft.EntityFrameworkCore;
using ProEvento.Dominio.Interfaces.Repositorios;
using ProEvento.Dominio.Models;
using ProEventos.Api.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ProEvento.Infraestrutura.Repositorio
{
    public class RepositorioPalestrantes : RepositorioBase<Palestrante>, IRepositorioPalestrantes
    {
        public RepositorioPalestrantes(ProEventoContext proEventoContext) : base(proEventoContext)
        {
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _proEventoContext.Palestrantes
                .Include(p => p.RedesSocials);

            if (includeEventos)
            {
                query.Include(p => p.PalestrantesEventos)
                    .ThenInclude(pe => pe.Evento);
            }

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _proEventoContext.Palestrantes
                .Include(p => p.RedesSocials)
                .OrderBy(p => p.Id)
                .Where(p => p.Nome.ToLower().Contains(nome.ToLower()));

            if (includeEventos)
            {
                query.Include(p => p.PalestrantesEventos)
                    .ThenInclude(pe => pe.Evento);
            }

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _proEventoContext.Palestrantes
                .Include(p => p.RedesSocials)
                .OrderBy(p => p.Id)
                .Where(p => p.Id == palestranteId);

            if (includeEventos)
            {
                query.Include(p => p.PalestrantesEventos)
                    .ThenInclude(pe => pe.Evento);
            }

            return await query.FirstOrDefaultAsync();
        }
    }
}