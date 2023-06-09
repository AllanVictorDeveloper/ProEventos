using ProEvento.Dominio.Interfaces.Repositorios;
using ProEvento.Dominio.Interfaces.Servicos;
using System;
using System.Threading.Tasks;

namespace ProEvento.Dominio.Servicos
{
    public class ServicoBase<T> : IServicoBase<T> where T : class
    {
        protected IRepositorioBase<T> _repositorioBase;


        public ServicoBase(IRepositorioBase<T> repositorioBase)
        {
            this._repositorioBase = repositorioBase;
        }

        public T Add(T objeto)
        {
            return _repositorioBase.Add(objeto);
        }

        public void Delete(T objeto)
        {
            _repositorioBase.Delete(objeto);
        }

        public void DeleteRange<T>(T[] objeto)
        {
            _repositorioBase.DeleteRange(objeto);
        }

        public void Dispose()
        {
            _repositorioBase.Dispose();
        }

        public bool SaveChanges()
        {
            return  _repositorioBase.SaveChanges();
        }

        public T Update(T objeto)
        {
            return _repositorioBase.Update(objeto);
        }
    }
}