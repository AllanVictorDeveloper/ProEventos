using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ProEvento.Dominio.Identity
{
    public class Role : IdentityRole<int>
    {
        public IEnumerable<UserRole> UserRoles { get; set; }
    }
}