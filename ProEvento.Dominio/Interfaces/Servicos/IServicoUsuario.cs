using ProEvento.Dominio.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProEvento.Dominio.Interfaces.Servicos
{
    public interface IServicoUsuario : IServicoBase<User>
    {
        Task<IEnumerable<User>> GetUsersAsync();

        Task<User> GetUserByIdAsync(int id);

        Task<User> GetUserByUsernameAsync(string username);
    }
}