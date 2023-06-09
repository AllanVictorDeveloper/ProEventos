using System.Threading.Tasks;

namespace ProEvento.Aplicacao.Interfaces.Servicos
{
    public interface IAppBase<T> where T : class
    {
        T Add(T objeto);

        T Update(T objeto);

        void Delete(T objeto);

        void DeleteRange<T>(T[] objeto);

        bool SaveChangesAsync();
    }
}