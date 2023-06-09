using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEvento.Dominio.Interfaces.Servicos
{
    public interface IServicoBase<T>: IDisposable where T : class
    {
        T Add(T objeto);

        T Update(T objeto);

        void Delete(T objeto);

        void DeleteRange<T>(T[] objeto);

        bool SaveChanges();
    }
}
