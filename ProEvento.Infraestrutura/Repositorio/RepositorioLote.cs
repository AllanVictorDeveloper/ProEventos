using Microsoft.EntityFrameworkCore;
using ProEvento.Dominio.Interfaces.Repositorios;
using ProEvento.Dominio.Models;
using ProEventos.Api.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProEvento.Infraestrutura.Repositorio
{
    public class RepositorioLote : RepositorioBase<Lote>, IRepositorioLote
    {
        public RepositorioLote(ProEventoContext proEventoContext) : base(proEventoContext)
        {
        }

       

        public async Task<Lote[]> GetLosteByEventoIdAsync(int eventoId)
        {
            IQueryable<Lote> query = _proEventoContext.Lotes;

            query = query.AsNoTracking()
                .Where(lote => lote.EventoId == eventoId);

            return await query.ToArrayAsync();
        }

        public async Task<Lote> GetLoteByIdsAsync(int eventoId, int loteId)
        {
            IQueryable<Lote> query = _proEventoContext.Lotes;

            query = query.AsNoTracking()
                .Where(lote => lote.EventoId == eventoId && lote.Id == loteId);

            return await query.FirstOrDefaultAsync();
        }
    }
}