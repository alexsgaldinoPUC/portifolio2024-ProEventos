using ProEventos.Domain.Models.RedesSociais;
using ProEventos.Persistence.Interfaces.Contratos.Geral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Interfaces.Contratos.RedesSociais
{
    public interface IRedeSocialPersistence : IGeralPersistence
    {
        Task<RedeSocial> GetRedeSocialEventoPorIdsAsync(int _eventoId, int _redeSocialId);
        Task<RedeSocial> GetRedeSocialPalestrantePorIdsAsync(int _palestranteId, int _redeSocialId);
        Task<RedeSocial[]> GetTodasRedesSociaisPorEventoIdAsync(int _eventoId);
        Task<RedeSocial[]> GetTodasRedesSociaisPorPalestranteIdAsync(int _palestranteId);
    }
}
