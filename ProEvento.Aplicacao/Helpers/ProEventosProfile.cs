using AutoMapper;
using ProEvento.Aplicacao.Dto;
using ProEvento.Dominio.Models;
using ProEventos.Domain.Models;

namespace ProEvento.Aplicacao.Helpers
{
    public class ProEventosProfile : Profile
    {
        public ProEventosProfile()
        {
            CreateMap<Evento, EventoRequest>().ReverseMap();
            CreateMap<EventoResponse, EventoRequest>().ReverseMap();
            CreateMap<Evento, EventoResponse>().ReverseMap();
            CreateMap<Lote, LoteResponse>().ReverseMap();
            CreateMap<Lote, LoteRequest>().ReverseMap();
        }
    }
}