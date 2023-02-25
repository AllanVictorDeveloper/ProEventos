using ProEvento.Aplicacao.Interfaces.Servicos;
using ProEvento.Dominio.Interfaces.Servicos;
using System;
using System.Threading.Tasks;

namespace ProEvento.Aplicacao.Servicos
{
    public class AppBase<T> : IAppBase<T> where T : class
    {
        private readonly IServicoBase<T> _servicoBase;

        public AppBase(IServicoBase<T> servicoBase)
        {
            _servicoBase = servicoBase;
        }

        public T Add(T objeto)
        {
            try
            {
                return _servicoBase.Add(objeto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(T objeto)
        {
            try
            {
                _servicoBase.Delete(objeto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteRange<T>(T[] objeto)
        {
            try
            {
                _servicoBase.DeleteRange(objeto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<bool> SaveChangesAsync()
        {
            try
            {
                return _servicoBase.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public T Update(T objeto)
        {
            try
            {
                return _servicoBase.Update(objeto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}