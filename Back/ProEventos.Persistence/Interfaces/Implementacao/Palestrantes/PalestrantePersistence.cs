using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Models.Palestrantes;
using ProEventos.Global.Models.Paginator;
using ProEventos.Persistence.Data;
using ProEventos.Persistence.Interfaces.Contratos.Palestrantes;
using ProEventos.Persistence.Interfaces.Implementacao.Geral;

namespace ProEventos.Persistence.Interfaces.Implementacao.Palestrantes
{
    public class PalestrantePersistence : GeralPersistence, IPalestrantePersistence
    {
        private readonly ProEventosContext context;
        public PalestrantePersistence(ProEventosContext _context) : base(_context)
        {
            context = _context;
        }
        public async Task<Palestrante> GetPalestrantePorUserIdAsync(int _userId, bool _incluirEventos = false)
        {
            IQueryable<Palestrante> query = context.Palestrantes
                .Include(p => p.User)
                .Include(p => p.RedesSociais)
                .AsNoTracking()
                .OrderBy(p => p.Id)
                .Where(p => p.UserId == _userId);

            if (_incluirEventos)
            {
                query = query.Include(e => e.PalestrantesEventos).ThenInclude(pe => pe.Evento);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<PageList<Palestrante>> GetTodosPalestrantesAsync(PageParams _pageParams, bool _incluirEventos = false)
        {
            IQueryable<Palestrante> query = context.Palestrantes
                .Include(p => p.User)
                .Include(p => p.RedesSociais)
                .AsNoTracking()
                .Where(p => (p.MiniCurriculo.ToLower().Contains(_pageParams.Term.ToLower()) ||
                             p.User.PrimeiroNome.ToLower().Contains(_pageParams.Term.ToLower()) ||
                             p.User.UltimoNome.ToLower().Contains(_pageParams.Term.ToLower())) &&
                             p.User.Funcao == Domain.Enums.Funcao.Palestrante)
                .OrderBy(p => p.Id);

            if (_incluirEventos)
            {
                query = query.Include(e => e.PalestrantesEventos).ThenInclude(pe => pe.Evento);
            }
            return await PageList<Palestrante>.CreateAsync(query, _pageParams.PageNumber, _pageParams.pageSize);
        }

    }
}
