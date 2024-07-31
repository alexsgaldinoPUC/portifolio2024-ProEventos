using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Models.Eventos;
using ProEventos.Persistence.Data;
using ProEventos.Persistence.Interfaces.Contratos.Eventos;
using ProEventos.Persistence.Interfaces.Contratos.Lotes;

namespace ProEventos.Persistence.Interfaces.Implementacao.Eventos
{
    public class EventoPersistence: IEventoPersistence
        {
        private readonly ProEventosContext context;

        public EventoPersistence(ProEventosContext _context)
        {
            context = _context;
        }

        public async Task<Evento> GetEventoPorIdAsync(int _eventoId, bool _incluirPalestrantes = false)
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

        public async Task<Evento[]> GetTodosEventosAsync(bool _incluirPalestrantes = false)
        {
            IQueryable<Evento> query = context.Eventos
                .Include(e => e.Lotes)
                .Include(e => e.RedesSociais)
                .AsNoTracking()
                .OrderBy(e => e.Id);

            if (_incluirPalestrantes) {
                query = query.Include(e => e.PalestrantesEventos).ThenInclude(pe => pe.Palestrante);
            }
            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetTodosEventosPorTemaAsync(string _tema, bool _incluirPalestrantes = false)
        {
            IQueryable<Evento> query = context.Eventos
                .Include(e => e.Lotes)
                .Include(e => e.RedesSociais)
                .AsNoTracking()
                .OrderBy(e => e.Id)
                .Where(e => e.Tema.ToLower().Contains(_tema.ToLower()));

            if (_incluirPalestrantes)
            {
                query = query.Include(e => e.PalestrantesEventos).ThenInclude(pe => pe.Palestrante);
            }
            return await query.ToArrayAsync();
        }
    }
}
