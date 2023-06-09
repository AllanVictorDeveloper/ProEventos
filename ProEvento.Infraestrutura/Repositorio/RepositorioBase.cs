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
                //_proEventoContext.Update(objeto);
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

        public T Delete(T objeto)
        {
            using var trans = _proEventoContext.Database.BeginTransaction();
            try
            {
                _proEventoContext.Set<T>().Remove(objeto);

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

        public void DeleteRange<T>(T[] objeto)
        {
            _proEventoContext.Set<T[]>().RemoveRange(objeto);
            _proEventoContext.SaveChanges();
        }

        public bool SaveChanges()
        {
            var retorno =  _proEventoContext.SaveChanges() > 0;

            return retorno;
        }

        public void Dispose()
        {
            _proEventoContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}