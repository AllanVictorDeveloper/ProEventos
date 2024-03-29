﻿using System.Collections.Generic;

namespace ProEvento.Dominio.Models
{
    public class Palestrante
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public string MiniCurriculo { get; set; }

        public string ImagemURL { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }

        public List<RedeSocial>? RedesSocials { get; set; }

        public List<PalestranteEvento>? PalestrantesEventos { get; set; }
    }
}