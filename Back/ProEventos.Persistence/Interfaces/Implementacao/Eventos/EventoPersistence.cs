using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Models.Eventos;
using ProEventos.Global.Models.Paginator;
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

        public async Task<Evento> GetEventoPorIdAsync(int _userId, int _eventoId, bool _incluirPalestrantes = false)
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

        public async Task<PageList<Evento>> GetTodosEventosAsync(int _userId, PageParams _pageParams, bool _incluirPalestrantes = false)
        {
            IQueryable<Evento> query = context.Eventos
                .Include(e => e.Lotes)
                .Include(e => e.RedesSociais)
                .AsNoTracking()
                .Where(e => (e.Tema.ToLower().Contains(_pageParams.Term.ToLower()) || 
                             e.Local.ToLower().Contains(_pageParams.Term.ToLower())) && e.UserId == _userId)
                .OrderBy(e => e.Id);

            if (_incluirPalestrantes)
            {
                query = query.Include(e => e.PalestrantesEventos).ThenInclude(pe => pe.Palestrante);
            }
            return await PageList<Evento>.CreateAsync(query, _pageParams.PageNumber, _pageParams.pageSize);
        }
    }
}
