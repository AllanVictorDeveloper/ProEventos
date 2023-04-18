using ProEvento.Dominio.Models;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace ProEvento.Aplicacao.Dto
{
    public class EventoRequest
    {
        public int Id { get; set; }
        public string Local { get; set; }
        public string DataEvento { get; set; }

        [Required(ErrorMessage ="O campo {0} é obrigatório.")]
        public string Tema { get; set; }
        public int QtdPessoas { get; set; }

        public string ImagemUrl { get; set; }

        public string Telefone { get; set; }

        [EmailAddress(ErrorMessage = "O campo {0} precisa ser um e-mail válido.")]
        public string Email { get; set; }

        public List<LoteRequest>? Lotes { get; set; }

        public List<RedeSocialRequest>? RedesSociais { get; set; }

        public List<PalestranteEventoRequest>? PalestrantesEventos { get; set; }
    }
}