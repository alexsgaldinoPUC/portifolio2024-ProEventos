using AutoMapper;
using ProEventos.Application.Dtos.Lotes;
using ProEventos.Application.Servicos.Contratos.Lotes;
using ProEventos.Application.Servicos.Implementacao.RedesSociais;
using ProEventos.Domain.Models.Lotes;
using ProEventos.Persistence.Interfaces.Contratos.Geral;
using ProEventos.Persistence.Interfaces.Contratos.Lotes;
using System.Text.Json;

namespace ProEventos.Application.Servicos.Implementacao.Lotes
{
    public class LoteServices : ILoteServices
    {
        private readonly ILotePersistence lotePersistence;
        private readonly IMapper mapper;

        public LoteServices(ILotePersistence _lotePersistence, IMapper _mapper)
        {
            lotePersistence = _lotePersistence;
            mapper = _mapper;
        }
        public async Task<bool> DeleteLote(int _eventoId, int _loteId)
        {
            try
            {
                var lote = await lotePersistence.GetLotePorIdsAsync(_eventoId, _loteId);

                if (lote == null) throw new Exception("Lote não encontrado para deleção.");

                lotePersistence.Delete<Lote>(lote);

                return await lotePersistence.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LoteDto> GetLotePorIdsAsync(int _eventoId, int _loteId)
        {
            try
            {
                var lote = await lotePersistence.GetLotePorIdsAsync(_eventoId, _loteId);

                if (lote == null) return null;

                var loteDto = mapper.Map<LoteDto>(lote);

                return loteDto;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<LoteDto[]> GetLotesPorEventoIdAsync(int _eventoId)
        {
            try
            {
                var lotes = await lotePersistence.GetLotesPorEventoIdAsync(_eventoId);

                if (lotes == null) return null;

                var lotesDto = mapper.Map<LoteDto[]>(lotes);

                return lotesDto;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<LoteDto[]> SaveLotes(int _eventoId, LoteDto[] _lotesDto)
        {
            try
            {
                Console.WriteLine("............Service...... " + _eventoId);
                var lotes = await lotePersistence.GetLotesPorEventoIdAsync(_eventoId);

                if (lotes == null) return null;

                foreach (var loteDto in _lotesDto)
                {
                    if (loteDto.Id == 0)
                    {
                        var lote = mapper.Map<Lote>(loteDto);

                        lote.EventoId = _eventoId;


                        lotePersistence.Update(lote);

                        await lotePersistence.SaveChangesAsync();

                    } else
                    {
                        var lote = lotes.FirstOrDefault(lote => lote.Id == loteDto.Id);
                        
                        lote.EventoId = _eventoId;

                        mapper.Map(lote, loteDto);

                        string json = JsonSerializer.Serialize(lote);

                        Console.WriteLine(json);

                        lotePersistence.Update(lote);

                        await lotePersistence.SaveChangesAsync();
                    }
                }

                var loteRetorno = await lotePersistence.GetLotesPorEventoIdAsync(_eventoId);
                var loteDtoRetorno = mapper.Map<LoteDto[]>(loteRetorno);
                return loteDtoRetorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
