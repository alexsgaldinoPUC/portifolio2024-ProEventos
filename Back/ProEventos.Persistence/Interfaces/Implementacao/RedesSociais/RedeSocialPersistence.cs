using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProEventos.Domain.Models.RedesSociais;
using ProEventos.Persistence.Data;
using ProEventos.Persistence.Interfaces.Contratos.RedesSociais;
using ProEventos.Persistence.Interfaces.Implementacao.Geral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Interfaces.Implementacao.RedesSociais
{
    public class RedeSocialPersistence : GeralPersistence, IRedeSocialPersistence
    {
        private readonly ProEventosContext context;

        public RedeSocialPersistence(ProEventosContext _context) : base(_context) 
        {
            context = _context;
        }
        public async Task<RedeSocial> GetRedeSocialEventoPorIdsAsync(int _eventoId, int _redeSocialId)
        {
            IQueryable<RedeSocial> query = context.RedesSociais;

            query = query.AsNoTracking()
                         .Where(rs => rs.EventoId == _eventoId && rs.Id == _redeSocialId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<RedeSocial> GetRedeSocialPalestrantePorIdsAsync(int _palestranteId, int _redeSocialId)
        {
            IQueryable<RedeSocial> query = context.RedesSociais;

            query = query.AsNoTracking()
                         .Where(rs => rs.PalestranteId == _palestranteId && rs.Id == _redeSocialId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<RedeSocial[]> GetTodasRedesSociaisPorEventoIdAsync(int _eventoId)
        {
            IQueryable<RedeSocial> query = context.RedesSociais;

            query = query.AsNoTracking()
                         .Where(rs => rs.EventoId == _eventoId);

            Console.WriteLine("||||||||||||||||||||||||||||||||||||||||" + (await query.ToArrayAsync()).Length);
            return await query.ToArrayAsync();
        }

        public async Task<RedeSocial[]> GetTodasRedesSociaisPorPalestranteIdAsync(int _palestranteId)
        {
            IQueryable<RedeSocial> query = context.RedesSociais;

            query = query.AsNoTracking()
             .Where(rs => rs.PalestranteId == _palestranteId);

            return await query.ToArrayAsync();
        }
    }
}
