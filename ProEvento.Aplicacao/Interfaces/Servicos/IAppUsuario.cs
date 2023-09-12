using ProEvento.Dominio.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProEvento.Aplicacao.Interfaces.Servicos
{
    public interface IAppUsuario : IAppBase<User>
    {
        Task<IEnumerable<User>> GetUsersAsync();

        Task<User> GetUserByIdAsync(int id);

        Task<User> GetUserByUsernameAsync(string username);
    }
}