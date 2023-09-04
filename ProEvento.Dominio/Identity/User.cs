using Microsoft.AspNetCore.Identity;
using ProEvento.Dominio.Enum;
using System.Collections.Generic;

namespace ProEvento.Dominio.Identity
{
    public class User : IdentityUser<int>
    {
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }
        public Titulo Titulo { get; set; }
        public string Descricao { get; set; }
        public Funcao Funcao { get; set; }

        public string ImagemURL { get; set; }

        public IEnumerable<UserRole> UserRoles { get; set; }
    }
}