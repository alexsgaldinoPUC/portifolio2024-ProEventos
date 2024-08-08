using ProEventos.Domain.Models.Palestrantes;
using ProEventos.Persistence.Interfaces.Contratos.Geral;

namespace ProEventos.Persistence.Interfaces.Contratos.Palestrantes
{
    public interface IPalestrantePersistence : IGeralPersistence
    {
        Task<Palestrante[]> GetTodosPalestrantesPorNomeAsync(string _nome, bool _incluirEventos = false);
        Task<Palestrante[]> GetTodosPalestrantesAsync(bool _incluirEventos = false);
        Task<Palestrante> GetPalestrantePorIdAsync(int _palestranteId, bool _incluirEventos = false);
    }
}
