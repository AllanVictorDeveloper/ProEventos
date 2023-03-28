using ProEvento.Dominio.Models;
using ProEventos.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEvento.Aplicacao.Dto
{
    public class PalestranteEventoRequest
    {
        public int PalestranteId { get; set; }

        public PalestranteRequest? Palestrante { get; set; }

        public int EventoId { get; set; }

        public EventoRequest? Evento { get; set; }
    }
}
