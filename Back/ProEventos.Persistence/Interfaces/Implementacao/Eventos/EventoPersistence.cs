using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Models.Eventos;
using ProEventos.Persistence.Data;
using ProEventos.Persistence.Interfaces.Contratos.Eventos;
using ProEventos.Persistence.Interfaces.Contratos.Lotes;
using ProEventos.Persistence.Interfaces.Implementacao.Geral;

namespace ProEventos.Persistence.Interfaces.Implementacao.Eventos
{
    public class EventoPersistence: GeralPersistence, IEventoPersistence 
        {
        private readonly ProEventosContext context;

        public EventoPersistence(ProEventosContext _context) : base(_context) 
        {
            context = _context;
        }

        public async Task<Evento> GetEventoPorIdAsync(int userId, int _eventoId, bool _incluirPalestrantes = false)
        {
            IQueryable<Evento> query = context.Eventos
                .Include(e => e.Lotes)
                .Include(e => e.RedesSociais)
                .AsNoTracking()
                .OrderBy(e => e.Id)
                .Where(e => e.Id == _eventoId);

            if (_incluirPalestrantes)
            {
                query = query.Include(e => e.PalestrantesEventos).ThenInclude(pe => pe.Palestrante);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Evento[]> GetTodosEventosAsync(int userId, bool _incluirPalestrantes = false)
        {
            IQueryable<Evento> query = context.Eventos
                .Include(e => e.Lotes)
                .Include(e => e.RedesSociais)
                .AsNoTracking()
                .Where(e => e.UserId == userId && e.UserId == userId)
                .OrderBy(e => e.Id);

            if (_incluirPalestrantes) {
                query = query.Include(e => e.PalestrantesEventos).ThenInclude(pe => pe.Palestrante);
            }
            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetTodosEventosPorTemaAsync(int userId, string _tema, bool _incluirPalestrantes = false)
        {
            IQueryable<Evento> query = context.Eventos
                .Include(e => e.Lotes)
                .Include(e => e.RedesSociais)
                .AsNoTracking()
                .Where(e => e.Tema.ToLower().Contains(_tema.ToLower()) && e.UserId == userId)
                .OrderBy(e => e.Id);

            if (_incluirPalestrantes)
            {
                query = query.Include(e => e.PalestrantesEventos).ThenInclude(pe => pe.Palestrante);
            }
            return await query.ToArrayAsync();
        }
    }
}
