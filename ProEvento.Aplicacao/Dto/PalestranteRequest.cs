using ProEvento.Dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEvento.Aplicacao.Dto
{
    public class PalestranteRequest
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public string MiniCurriculo { get; set; }

        public string ImagemURL { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }

        public List<RedeSocialRequest>? RedesSocials { get; set; }

        public List<PalestranteEventoRequest>? PalestrantesEventos { get; set; }
    }
}
