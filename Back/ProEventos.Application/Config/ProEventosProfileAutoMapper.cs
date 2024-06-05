using AutoMapper;
using ProEventos.Application.Dtos.Eventos;
using ProEventos.Application.Dtos.Lotes;
using ProEventos.Application.Dtos.Palestrantes;
using ProEventos.Application.Dtos.RedesSociais;
using ProEventos.Domain.Models.Eventos;
using ProEventos.Domain.Models.Lotes;
using ProEventos.Domain.Models.Palestrantes;
using ProEventos.Domain.Models.RedesSociais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Application.Config
{
    public class ProEventosProfileAutoMapper : Profile
    {
        public ProEventosProfileAutoMapper()
        {
            CreateMap<Evento, EventoDto>().ReverseMap();
            CreateMap<Lote, LoteDto>().ReverseMap();
            CreateMap<Palestrante, PalestranteDto>().ReverseMap();
            CreateMap<PalestranteEvento, PalestranteEventoDto>().ReverseMap();
            CreateMap<RedeSocial, RedeSocialDto>().ReverseMap();
        }
    }
}
