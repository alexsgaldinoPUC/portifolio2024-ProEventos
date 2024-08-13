using ProEventos.Application.Dtos.Eventos;
using ProEventos.Global.Models.Paginator;

namespace ProEventos.Application.Servicos.Contratos.Eventos
{
    public interface IEventoServices
    {
        Task<EventoDto> AddEventos(int _userId, EventoDto _eventoDto);
        Task<EventoDto> UpdateEvento(int _userId, int _eventoId, EventoDto _eventoDto);
        Task<bool> DeleteEvento(int _userId, int _eventoId);

        Task<PageList<EventoDto>> GetTodosEventosAsync(int _userId, PageParams _pageParams, bool _incluirPalestrantes = false);
        Task<EventoDto> GetEventoPorIdAsync(int _userId, int _eventoId, bool _incluirPalestrantes = false);    }
}
