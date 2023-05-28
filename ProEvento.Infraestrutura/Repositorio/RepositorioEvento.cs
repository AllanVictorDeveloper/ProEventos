using Microsoft.EntityFrameworkCore;
using ProEvento.Dominio.Interfaces.Repositorios;
using ProEventos.Api.Data;
using ProEventos.Domain.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProEvento.Infraestrutura.Repositorio
{
    public class RepositorioEvento : RepositorioBase<Evento>, IRepositorioEventos
    {
        public RepositorioEvento(ProEventoContext proEventoContext) : base(proEventoContext)
        {
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Evento GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            try
            {
                var evento = _proEventoContext.Eventos
                .Include(e => e.Lotes)
                .Include(e => e.RedesSociais)
                .OrderBy(e => e.Id)
                .Where(e => e.Id == eventoId).AsNoTracking();

                if (includePalestrantes)
                {
                    evento.Include(e => e.PalestrantesEventos)
                        .ThenInclude(pe => pe.Palestrante);
                }

                return evento.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            //IQueryable<Evento> query = _proEventoContext.Eventos
            //    .Include(e => e.Lotes)
            //    .Include(e => e.RedesSociais)
            //    .OrderBy(e => e.Id)
            //    .Where(e => e.Id == eventoId);

            //if (includePalestrantes)
            //{
            //    query.Include(e => e.PalestrantesEventos)
            //        .ThenInclude(pe => pe.Palestrante);
            //}
        }
    }
}