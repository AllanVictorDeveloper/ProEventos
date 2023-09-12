using ProEvento.Dominio.Identity;
using ProEvento.Dominio.Interfaces.Repositorios;
using ProEvento.Dominio.Interfaces.Servicos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProEvento.Dominio.Servicos
{
    public class ServicoUsuario : ServicoBase<User>, IServicoUsuario
    {
        private readonly IRepositorioUsuario _repositorioUsuario;

        public ServicoUsuario(IRepositorioUsuario repositorioUsuario) : base(repositorioUsuario)
        {
            _repositorioUsuario = repositorioUsuario;
        }

        public Task<User> GetUserByIdAsync(int id)
        {
            return _repositorioUsuario.GetUserByIdAsync(id);
        }

        public Task<User> GetUserByUsernameAsync(string username)
        {
            return _repositorioUsuario.GetUserByUsernameAsync(username);
        }

        public Task<IEnumerable<User>> GetUsersAsync()
        {
            return _repositorioUsuario.GetUsersAsync();
        }
    }
}