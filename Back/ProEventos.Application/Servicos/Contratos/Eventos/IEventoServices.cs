using ProEventos.Application.Dtos.Eventos;

namespace ProEventos.Application.Servicos.Contratos.Eventos
{
    public interface IEventoServices
    {
        Task<EventoDto> AddEventos(EventoDto _eventoDto);
        Task<EventoDto> UpdateEvento(int _eventoId, EventoDto _eventoDto);
        Task<bool> DeleteEvento(int _eventoId);

        Task<EventoDto[]> GetTodosEventosPorTemaAsync(string _tema, bool _incluirPalestrantes = false);
        Task<EventoDto[]> GetTodosEventosAsync(bool _incluirPalestrantes = false);
        Task<EventoDto> GetEventoPorIdAsync(int _eventoId, bool _incluirPalestrantes = false);    }
}
