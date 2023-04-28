using Microsoft.EntityFrameworkCore;
using ProEvento.Dominio.Interfaces.Repositorios;
using ProEventos.Api.Data;
using System;
using System.Threading.Tasks;

namespace ProEvento.Infraestrutura.Repositorio
{
    public class RepositorioBase<T> : IRepositorioBase<T> where T : class
    {
        protected readonly ProEventoContext _proEventoContext;

        public RepositorioBase(ProEventoContext proEventoContext)
        {
            _proEventoContext = proEventoContext;
        }

        public T Add(T objeto)
        {
            using var trans = _proEventoContext.Database.BeginTransaction();
            try
            {
                _proEventoContext.Entry(objeto).State = EntityState.Added;

                _proEventoContext.SaveChanges();
                trans.Commit();
                return objeto;
            }
            catch (Exception err)
            {
                trans.Rollback();
                throw err;
            }
        }

        public T Update(T objeto)
        {
            using var trans = _proEventoContext.Database.BeginTransaction();
            try
            {
                _proEventoContext.Entry(objeto).State = EntityState.Modified;

                _proEventoContext.SaveChanges();
                trans.Commit();
                return objeto;
            }
            catch (Exception err)
            {
                trans.Rollback();
                throw err;
            }
        }

        public void Delete(T objeto)
        {
            using var trans = _proEventoContext.Database.BeginTransaction();
            try
            {
                _proEventoContext.Entry(objeto).State = EntityState.Deleted;

                _proEventoContext.SaveChanges();
                trans.Commit();
            }
            catch (Exception err)
            {
                trans.Rollback();
                throw err;
            }
        }

        public void DeleteRange<T>(T[] objeto)
        {
            _proEventoContext.Set<T[]>().RemoveRange(objeto);
            _proEventoContext.SaveChanges();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _proEventoContext.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _proEventoContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}