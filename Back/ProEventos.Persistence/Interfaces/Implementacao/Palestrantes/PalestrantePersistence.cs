using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Models.Palestrantes;
using ProEventos.Persistence.Data;
using ProEventos.Persistence.Interfaces.Contratos.Palestrantes;

namespace ProEventos.Persistence.Interfaces.Implementacao.Palestrantes
{
    internal class PalestrantePersistence : IPalestrantePersistence
    {
        private readonly ProEventosContext context;
        public PalestrantePersistence(ProEventosContext _context)
        {
            context = _context;
        }
        public async Task<Palestrante> GetPalestrantePorIdAsync(int _palestranteId, bool _incluirEventos = false)
        {
            IQueryable<Palestrante> query = context.Palestrantes
                .Include(p => p.RedesSociais)
                .AsNoTracking()
                .OrderBy(p => p.Id)
                .Where(p => p.Id == _palestranteId);

            if (_incluirEventos)
            {
                query = query.Include(e => e.PalestrantesEventos).ThenInclude(pe => pe.Evento);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Palestrante[]> GetTodosPalestrantesAsync(bool _incluirEventos = false)
        {
            IQueryable<Palestrante> query = context.Palestrantes
                .Include(p => p.RedesSociais)
                .AsNoTracking()
                .OrderBy(p => p.Id);

            if (_incluirEventos)
            {
                query = query.Include(e => e.PalestrantesEventos).ThenInclude(pe => pe.Evento);
            }
            return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetTodosPalestrantesPorNomeAsync(string _nome, bool _incluirEventos = false)
        {
            IQueryable<Palestrante> query = context.Palestrantes
                .Include(p => p.RedesSociais)
                .AsNoTracking()
                .OrderBy(p => p.Id)
                .Where(e => e.Nome.ToLower().Contains(_nome.ToLower()));

            if (_incluirEventos)
            {
                query = query.Include(e => e.PalestrantesEventos).ThenInclude(pe => pe.Palestrante);
            }
            return await query.ToArrayAsync();
        }
    }
}
