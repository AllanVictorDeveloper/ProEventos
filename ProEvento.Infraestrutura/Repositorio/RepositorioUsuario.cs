using Microsoft.EntityFrameworkCore;
using ProEvento.Dominio.Identity;
using ProEvento.Dominio.Interfaces.Repositorios;
using ProEventos.Api.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProEvento.Infraestrutura.Repositorio
{
    public class RepositorioUsuario : RepositorioBase<User>, IRepositorioUsuario
    {
        public RepositorioUsuario(ProEventoContext proEventoContext) : base(proEventoContext)
        {
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _proEventoContext.Users.SingleOrDefaultAsync(u => u.Id.Equals(id));
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _proEventoContext.Users.Where(i => i.UserName == username.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _proEventoContext.Users.ToListAsync();
        }
    }
}