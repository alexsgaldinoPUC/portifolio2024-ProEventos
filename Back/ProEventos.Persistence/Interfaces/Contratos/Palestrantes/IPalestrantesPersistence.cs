using ProEventos.Domain.Eventos;
using ProEventos.Domain.Palestrantes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Interfaces.Contratos.Palestrantes
{
    public interface IPalestrantesPersistence
    {
        Task<Palestrante[]> GetTodosPalestrantesPorNomeAsync(string _nome, bool _incluirEventos = false);
        Task<Palestrante[]> GetTodosPalestrantesAsync(bool _incluirEventos = false);
        Task<Palestrante> GetPalestrantePorIdAsync(int _palestranteId, bool _incluirEventos = false);
    }
}
