using ProEvento.Dominio.Identity;
using System.Collections.Generic;

namespace ProEvento.Dominio.Models
{
    public class Palestrante
    {
        public int Id { get; set; }

        public string MiniCurriculo { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public List<RedeSocial>? RedesSocials { get; set; }

        public List<PalestranteEvento>? PalestrantesEventos { get; set; }
    }
}