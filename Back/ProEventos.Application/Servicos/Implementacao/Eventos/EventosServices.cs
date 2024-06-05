using AutoMapper;
using ProEventos.Application.Dtos.Eventos;
using ProEventos.Application.Servicos.Contratos.Eventos;
using ProEventos.Domain.Models.Eventos;
using ProEventos.Persistence.Interfaces.Contratos.Eventos;
using ProEventos.Persistence.Interfaces.Contratos.Geral;

namespace ProEventos.Application.Servicos.Implementacao.Eventos
{
    public class EventosServices : IEventosServices
    {
        private readonly IGeralPersistence geralPersistence;
        private readonly IEventosPersistence eventosPersistence;
        private readonly IMapper mapper;

        public EventosServices(IGeralPersistence _geralPersistence, IEventosPersistence _eventosPersistence, IMapper _mapper)
        {
            geralPersistence = _geralPersistence;
            eventosPersistence = _eventosPersistence;
            mapper = _mapper;
        }
        public async Task<EventoDto> AddEventos(EventoDto _eventoDto)
        {
            try
            {
                var evento = mapper.Map<Evento>(_eventoDto);

                geralPersistence.Add<Evento>(evento);

                if (await geralPersistence.SaveChangesAsync())
                {
                    var eventoRetorno = await eventosPersistence.GetEventoPorIdAsync(evento.Id, false);
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

        public async Task<bool> DeleteEvento(int _eventoId)
        {
            try
            {
                var evento = await eventosPersistence.GetEventoPorIdAsync(_eventoId, false);

                if (evento == null) throw new Exception("Evento não encontrado para deleção.");

                geralPersistence.Delete<Evento>(evento);

                return await geralPersistence.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto> GetEventoPorIdAsync(int _eventoId, bool _incluirPalestrantes = false)
        {
            try
            {
                var evento = await eventosPersistence.GetEventoPorIdAsync(_eventoId, _incluirPalestrantes);

                if (evento == null) return null;

                var eventoDto = mapper.Map<EventoDto>(evento);

                return eventoDto;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<EventoDto[]> GetTodosEventosAsync(bool _incluirPalestrantes = false)
        {
            try
            {
                var eventos = await eventosPersistence.GetTodosEventosAsync(_incluirPalestrantes);

                if (eventos == null) return null;

                var eventosDto = mapper.Map<EventoDto[]>(eventos);

                return eventosDto;
            }
            catch(Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<EventoDto[]> GetTodosEventosPorTemaAsync(string _tema, bool _incluirPalestrantes = false)
        {
            try
            {
                var eventos = await eventosPersistence.GetTodosEventosPorTemaAsync(_tema, _incluirPalestrantes);

                if (eventos == null) return null;

                var eventosDto = mapper.Map<EventoDto[]>(eventos);

                return eventosDto;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<EventoDto> UpdateEvento(int _eventoId, EventoDto _eventoDto)
        {
            try
            {
                var evento = await eventosPersistence.GetEventoPorIdAsync(_eventoId, false);

                if (evento == null) return null;

                _eventoDto.Id = evento.Id;

                mapper.Map(_eventoDto, evento);

                geralPersistence.Update<Evento>(evento);

                if (await geralPersistence.SaveChangesAsync())
                {
                    var eventoRetorno = await eventosPersistence.GetEventoPorIdAsync(evento.Id, false);
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
