using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Models.Lotes;
using ProEventos.Persistence.Data;
using ProEventos.Persistence.Interfaces.Contratos.Lotes;
using ProEventos.Persistence.Interfaces.Implementacao.Geral;

namespace ProEventos.Persistence.Interfaces.Implementacao.Lotes
{
    public class LotePersistence : GeralPersistence, ILotePersistence
    {
        private readonly ProEventosContext context;

        public LotePersistence(ProEventosContext _context) : base(_context)
        {
            context = _context;
        }

        public async Task<Lote> GetLotePorIdsAsync(int _eventoId, int _loteId)
        {
            IQueryable<Lote> query = context.Lotes;

            query = query.AsNoTracking()
                         .Where(lote => lote.EventoId == _eventoId && lote.Id == _loteId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Lote[]> GetLotesPorEventoIdAsync(int _eventoId)
        {
            IQueryable<Lote> query = context.Lotes;

            query = query.AsNoTracking()
                         .Where(lote => lote.EventoId == _eventoId);

            return await query.ToArrayAsync();
        }
    }
}
