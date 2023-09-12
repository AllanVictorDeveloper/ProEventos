using ProEvento.Dominio.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProEvento.Dominio.Interfaces.Repositorios
{
    public interface IRepositorioUsuario : IRepositorioBase<User>
    {
        Task<IEnumerable<User>> GetUsersAsync();

        Task<User> GetUserByIdAsync(int id);

        Task<User> GetUserByUsernameAsync(string username);
    }
}