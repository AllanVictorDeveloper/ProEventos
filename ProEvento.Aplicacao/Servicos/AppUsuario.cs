using ProEvento.Aplicacao.Interfaces.Servicos;
using ProEvento.Dominio.Identity;
using ProEvento.Dominio.Interfaces.Servicos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProEvento.Aplicacao.Servicos
{
    public class AppUsuario : AppBase<User>, IAppUsuario
    {
        private readonly IServicoUsuario _servicoUser;

        public AppUsuario(IServicoUsuario servicoUser) : base(servicoUser)
        {
            _servicoUser = servicoUser;
        }

        public Task<User> GetUserByIdAsync(int id)
        {
            return _servicoUser.GetUserByIdAsync(id);
        }

        public Task<User> GetUserByUsernameAsync(string username)
        {
            return _servicoUser.GetUserByUsernameAsync(username);
        }

        public Task<IEnumerable<User>> GetUsersAsync()
        {
            return _servicoUser.GetUsersAsync();
        }
    }
}