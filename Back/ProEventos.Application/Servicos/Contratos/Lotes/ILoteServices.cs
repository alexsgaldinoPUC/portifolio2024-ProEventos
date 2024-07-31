using ProEventos.Application.Dtos.Lotes;

namespace ProEventos.Application.Servicos.Contratos.Lotes
{
    public interface ILoteServices
    {
        Task<LoteDto[]> SaveLotes(int _eventoId, LoteDto[] _LoteDto);
        Task<bool> DeleteLote(int _eventoId, int _loteId);

        Task<LoteDto[]> GetLotesPorEventoIdAsync(int _eventoId);
        Task<LoteDto> GetLotePorIdsAsync(int _eventoId, int loteId);
    }
}
