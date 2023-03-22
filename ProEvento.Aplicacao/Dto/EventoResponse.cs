using ProEvento.Dominio.Models;
using System.Collections.Generic;

namespace ProEvento.Aplicacao.Dto
{
    public class EventoResponse
    {
        public int Id { get; set; }
        public string Local { get; set; }
        public string DataEvento { get; set; }

        public string Tema { get; set; }
        public int QtdPessoas { get; set; }

        public string ImagemUrl { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }

        //public List<Lote>? Lotes { get; set; }

        //public List<RedeSocial>? RedesSociais { get; set; }

        //public List<PalestranteEvento>? PalestrantesEventos { get; set; }
    }
}