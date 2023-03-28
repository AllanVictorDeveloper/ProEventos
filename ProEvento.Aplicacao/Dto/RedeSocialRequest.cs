using ProEvento.Dominio.Models;
using ProEventos.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEvento.Aplicacao.Dto
{
    public class RedeSocialRequest
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string URL { get; set; }

        public int? EventoId { get; set; }

        public EventoRequest? Evento { get; set; }
        public int PalestranteId { get; set; }

        public PalestranteRequest? Palestrante { get; set; }

    }
}
