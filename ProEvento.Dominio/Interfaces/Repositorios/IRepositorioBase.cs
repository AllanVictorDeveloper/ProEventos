using System;
using System.Threading.Tasks;

namespace ProEvento.Dominio.Interfaces.Repositorios
{
    public interface IRepositorioBase<T>: IDisposable where T : class
    {
        T Add(T objeto);

        T Update(T objeto);

        T Delete(T objeto);

        void DeleteRange<T>(T[] objeto);

        bool SaveChanges();
    }
}