using AutoMapper;
using ProEventos.Application.Dtos.Eventos;
using ProEventos.Application.Servicos.Contratos.Eventos;
using ProEventos.Application.Servicos.Contratos.Lotes;
using ProEventos.Domain.Models.Eventos;
using ProEventos.Global.Models.Paginator;
using ProEventos.Persistence.Interfaces.Contratos.Eventos;
using ProEventos.Persistence.Interfaces.Contratos.Geral;
using ProEventos.Persistence.Interfaces.Contratos.Lotes;

namespace ProEventos.Application.Servicos.Implementacao.Eventos
{
    public class EventoServices : IEventoServices
    {
        private readonly IGeralPersistence geralPersistence;
        private readonly IEventoPersistence eventosPersistence;
        private readonly IMapper mapper;

        public EventoServices(IGeralPersistence _geralPersistence, IEventoPersistence _eventosPersistence, IMapper _mapper)
        {
            geralPersistence = _geralPersistence;
            eventosPersistence = _eventosPersistence;
            mapper = _mapper;
        }
        public async Task<EventoDto> AddEventos(int userId, EventoDto _eventoDto)
        {
            try
            {
                var evento = mapper.Map<Evento>(_eventoDto);

                evento.UserId = userId;

                geralPersistence.Add<Evento>(evento);

                if (await geralPersistence.SaveChangesAsync())
                {
                    var eventoRetorno = await eventosPersistence.GetEventoPorIdAsync(userId, evento.Id, false);
                    var eventoDtoRetorno = mapper.Map<EventoDto>(eventoRetorno);
                    return eventoDtoRetorno;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteEvento(int _userId, int _eventoId)
        {
            try
            {
                var evento = await eventosPersistence.GetEventoPorIdAsync(_userId, _eventoId, false);

                if (evento == null) throw new Exception("Evento não encontrado para deleção.");

                geralPersistence.Delete<Evento>(evento);

                return await geralPersistence.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto> GetEventoPorIdAsync(int _userId, int _eventoId, bool _incluirPalestrantes = false)
        {
            try
            {
                var evento = await eventosPersistence.GetEventoPorIdAsync(_userId, _eventoId, _incluirPalestrantes);

                if (evento == null) return null;

                var eventoDto = mapper.Map<EventoDto>(evento);

                return eventoDto;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<PageList<EventoDto>> GetTodosEventosAsync(int _userId, PageParams _pageParams, bool _incluirPalestrantes = false)
        {
            try
            {
                var eventos = await eventosPersistence.GetTodosEventosAsync(_userId, _pageParams, _incluirPalestrantes);

                if (eventos == null) return null;

                var eventosDto = mapper.Map<PageList<EventoDto>>(eventos);

                eventosDto.CurrentPage = eventos.CurrentPage;
                eventosDto.TotalPages = eventos.TotalPages;
                eventosDto.PageSize = eventos.PageSize;
                eventosDto.TotalCount = eventos.TotalCount;

                return eventosDto;
            }
            catch(Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<EventoDto> UpdateEvento(int userId, int _eventoId, EventoDto _eventoDto)
        {
            try
            {
                var evento = await eventosPersistence.GetEventoPorIdAsync(userId, _eventoId, false);

                if (evento == null) return null;

                _eventoDto.Id = evento.Id;
                _eventoDto.UserId = userId;

                mapper.Map(_eventoDto, evento);

                geralPersistence.Update<Evento>(evento);

                if (await geralPersistence.SaveChangesAsync())
                {
                    var eventoRetorno = await eventosPersistence.GetEventoPorIdAsync(userId, evento.Id, false);
                    var eventoDtoRetorno = mapper.Map<EventoDto>(eventoRetorno);
                    return eventoDtoRetorno;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
