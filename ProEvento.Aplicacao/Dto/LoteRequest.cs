using ProEvento.Dominio.Models;
using System;

namespace ProEvento.Aplicacao.Dto
{
    public class LoteRequest
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public decimal Preco { get; set; }

        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }

        public int Quantidade { get; set; }

        public int EventoId { get; set; }
        public EventoRequest? Evento { get; set; }

        public Lote ToObject()
        {
            Lote obj = new Lote
            {
                Id = Id,
                Nome = Nome,
                Preco = Preco,
                DataInicio = DataInicio,
                DataFim = DataFim,
                Quantidade = Quantidade,
                EventoId = EventoId,
            };

            return obj;
        }
    }
}