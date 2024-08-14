using ProEventos.Application.Dtos.RedesSociais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Application.Servicos.Contratos.RedesSociais
{
    public interface IRedeSocialServices 
    {
        Task<RedeSocialDto[]> SavePorEvento(int _eventoId, RedeSocialDto[] _redeSocialDto);
        Task<bool> DeletePorEvento(int _eventoId, int _redeSocialId);
        Task<RedeSocialDto[]> SavePorPalestrante(int _palestranteId, RedeSocialDto[] _redeSocialDto);
        Task<bool> DeletePorPalestrante(int _palestranteId, int _redeSocialId);
        Task<RedeSocialDto[]> GetTodasRedesSociaisPorEventoIdAsync(int _eventoId);
        Task<RedeSocialDto[]> GetTodasRedesSociaisPorPalestranteIdAsync(int _palestranteId);
        Task<RedeSocialDto> GetRedeSocialEventoPorIdsAsync(int _eventoId, int _redeSocialId);
        Task<RedeSocialDto> GetRedeSocialPalestrantePorIdsAsync(int _palestranteId, int _redeSocialId);
    }
}
