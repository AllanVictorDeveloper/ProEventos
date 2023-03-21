using AutoMapper;
using ProEvento.Aplicacao.Dto;
using ProEventos.Domain.Models;

namespace ProEventos.Api.Helpers
{
    public class ProEventosProfile : Profile
    {
        public ProEventosProfile()
        {
            CreateMap<Evento, EventoResponse>();
        }
    }
}