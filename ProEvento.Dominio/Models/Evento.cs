﻿using ProEvento.Dominio.Identity;
using ProEvento.Dominio.Models;
using System;
using System.Collections.Generic;

namespace ProEventos.Domain.Models
{
    public class Evento
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Local { get; set; }
        public DateTime? DataEvento { get; set; }

        public string Tema { get; set; }
        public int QtdPessoas { get; set; }

        public string ImagemUrl { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }

        public List<Lote>? Lotes { get; set; }

        public List<RedeSocial>? RedesSociais { get; set; }

        public List<PalestranteEvento>? PalestrantesEventos { get; set; }
    }
}