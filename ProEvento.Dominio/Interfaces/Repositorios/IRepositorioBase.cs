using System;
using System.Threading.Tasks;

namespace ProEvento.Dominio.Interfaces.Repositorios
{
    public interface IRepositorioBase<T>: IDisposable where T : class
    {
        T Add(T objeto);

        T Update(T objeto);

        void Delete(T objeto);

        void DeleteRange<T>(T[] objeto);

        Task<bool> SaveChangesAsync();
    }
}