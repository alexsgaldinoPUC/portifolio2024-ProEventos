using ProEventos.Domain.Models.Palestrantes;
using ProEventos.Global.Models.Paginator;
using ProEventos.Persistence.Interfaces.Contratos.Geral;

namespace ProEventos.Persistence.Interfaces.Contratos.Palestrantes
{
    public interface IPalestrantePersistence : IGeralPersistence
    {
        Task<PageList<Palestrante>> GetTodosPalestrantesAsync(PageParams _pageParams, bool _incluirEventos = false);
        Task<Palestrante> GetPalestrantePorUserIdAsync(int _userId, bool _incluirEventos = false);
    }
}
