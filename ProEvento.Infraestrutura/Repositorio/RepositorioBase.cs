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
            _proEventoContext.Set<T>().Add(objeto);
            _proEventoContext.SaveChanges();
            return objeto;
        }

        public T Update(T objeto)
        {
            _proEventoContext.Set<T>().Update(objeto);
            _proEventoContext.SaveChanges();
            return objeto;
        }

        public void Delete(T objeto)
        {
            _proEventoContext.Set<T>().Remove(objeto);
            _proEventoContext.SaveChanges();
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
            GC.Collect();
        }
    }
}