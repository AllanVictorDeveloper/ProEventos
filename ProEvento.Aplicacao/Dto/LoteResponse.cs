using ProEventos.Domain.Models;
using System;

namespace ProEvento.Aplicacao.Dto
{
    public class LoteResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public decimal Preco { get; set; }

        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }

        public int Quantidade { get; set; }

        public int EventoId { get; set; }
        public Evento? Evento { get; set; }
    }
}