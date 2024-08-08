using ProEventos.Application.Dtos.Eventos;

namespace ProEventos.Application.Servicos.Contratos.Eventos
{
    public interface IEventoServices
    {
        Task<EventoDto> AddEventos(int _userId, EventoDto _eventoDto);
        Task<EventoDto> UpdateEvento(int _userId, int _eventoId, EventoDto _eventoDto);
        Task<bool> DeleteEvento(int _userId, int _eventoId);

        Task<EventoDto[]> GetTodosEventosPorTemaAsync(int _userId, string _tema, bool _incluirPalestrantes = false);
        Task<EventoDto[]> GetTodosEventosAsync(int _userId, bool _incluirPalestrantes = false);
        Task<EventoDto> GetEventoPorIdAsync(int _userId, int _eventoId, bool _incluirPalestrantes = false);    }
}
