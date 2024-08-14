using ProEventos.Application.Dtos.Palestrantes;
using ProEventos.Global.Models.Paginator;

namespace ProEventos.Application.Servicos.Contratos.Palestrantes
{
    public interface IPalestranteServices
    {
        Task<PalestranteAddDto> AddPalestrantes(int _userId, PalestranteAddDto _palestranteDto);
        Task<PalestranteUpdateDto> UpdatePalestrante(int _userId, PalestranteUpdateDto _palestranteDto);

        Task<PageList<PalestranteDto>> GetTodosPalestrantesAsync(PageParams _pageParams, bool _incluirEventos = false);
        Task<PalestranteDto> GetPalestrantePorUserIdAsync(int _userId, bool _incluirEventos = false);
    }
}
