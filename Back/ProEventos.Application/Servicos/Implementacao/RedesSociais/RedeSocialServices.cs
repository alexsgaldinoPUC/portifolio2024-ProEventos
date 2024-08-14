using AutoMapper;
using ProEventos.Application.Dtos.Lotes;
using ProEventos.Application.Dtos.RedesSociais;
using ProEventos.Application.Servicos.Contratos.Lotes;
using ProEventos.Application.Servicos.Contratos.RedesSociais;
using ProEventos.Domain.Models.Lotes;
using ProEventos.Domain.Models.RedesSociais;
using ProEventos.Persistence.Interfaces.Contratos.RedesSociais;
using System.Text.Json;

namespace ProEventos.Application.Servicos.Implementacao.RedesSociais
{
    public class RedeSocialServices : IRedeSocialServices
    {
        private readonly IRedeSocialPersistence redeSocialPersistence;
        private readonly IMapper mapper;

        public RedeSocialServices(IRedeSocialPersistence _redeSocialPersistence, IMapper _mapper)
        {
            redeSocialPersistence = _redeSocialPersistence;
            mapper = _mapper;
        }
        public async Task AddRedeSocial(int _id, RedeSocialDto _redeSocialDto, bool isEvento)
        {
            try
            {
                var redeSocial = mapper.Map<RedeSocial>(_redeSocialDto);
                Console.WriteLine("===================================== " + isEvento);

                if (isEvento) 
                {
                    redeSocial.EventoId = _id;
                    redeSocial.PalestranteId = null;
                } 
                else
                {
                    redeSocial.EventoId = null;
                    redeSocial.PalestranteId = _id;

                }

                redeSocialPersistence.Add<RedeSocial>(redeSocial);

                await redeSocialPersistence.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> DeletePorEvento(int _eventoId, int _redeSocialId)
        {
            try
            {
                var redeSocial = await redeSocialPersistence.GetRedeSocialEventoPorIdsAsync(_eventoId, _redeSocialId);

                if (redeSocial == null) throw new Exception("RedeSocial por Evento não encontrada para deleção.");

                redeSocialPersistence.Delete(redeSocial);

                return await redeSocialPersistence.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeletePorPalestrante(int _palestranteId, int _redeSocialId)
        {
            try
            {
                var redeSocial = await redeSocialPersistence.GetRedeSocialPalestrantePorIdsAsync(_palestranteId, _redeSocialId);

                if (redeSocial == null) throw new Exception("RedeSocial por Palestrante não encontrada para deleção.");

                redeSocialPersistence.Delete(redeSocial);

                return await redeSocialPersistence.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RedeSocialDto> GetRedeSocialEventoPorIdsAsync(int _eventoId, int _redeSocialId)
        {
            try
            {
                var redeSocial = await redeSocialPersistence.GetRedeSocialEventoPorIdsAsync(_eventoId, _redeSocialId);

                if (redeSocial == null) return null;

                var redeSocialDto = mapper.Map<RedeSocialDto>(redeSocial);

                return redeSocialDto;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<RedeSocialDto> GetRedeSocialPalestrantePorIdsAsync(int _palestrante, int _redeSocialId)
        {
            try
            {
                var redeSocial = await redeSocialPersistence.GetRedeSocialPalestrantePorIdsAsync(_palestrante, _redeSocialId);

                if (redeSocial == null) return null;

                var redeSocialDto = mapper.Map<RedeSocialDto>(redeSocial);

                return redeSocialDto;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<RedeSocialDto[]> GetTodasRedesSociaisPorEventoIdAsync(int _eventoId)
        {
            try
            {
                var redesSociais = await redeSocialPersistence.GetTodasRedesSociaisPorEventoIdAsync(_eventoId);

                if (redesSociais == null) return null;

                var reddesSociaisDto = mapper.Map<RedeSocialDto[]>(redesSociais);

                return reddesSociaisDto;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<RedeSocialDto[]> GetTodasRedesSociaisPorPalestranteIdAsync(int _palestranteId)
        {
            try
            {
                var redesSociais = await redeSocialPersistence.GetTodasRedesSociaisPorPalestranteIdAsync(_palestranteId);

                if (redesSociais == null) return null;

                var reddesSociaisDto = mapper.Map<RedeSocialDto[]>(redesSociais);

                return reddesSociaisDto;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<RedeSocialDto[]> SavePorEvento(int _eventoId, RedeSocialDto[] _redesSociaisDto)
        {
            try
            {
                var redesSociais = await redeSocialPersistence.GetTodasRedesSociaisPorEventoIdAsync(_eventoId);

                Console.WriteLine("............Service...... " + _redesSociaisDto[0].Id);

                if (redesSociais == null) return null;

                foreach (var redeSocialDto in _redesSociaisDto)
                {
                    if (redeSocialDto.Id == 0 )
                    {
                        await AddRedeSocial(_eventoId, redeSocialDto, true);

                    }
                    else
                    {
                        var redeSocial = redesSociais.FirstOrDefault(redeSocial => redeSocial.Id == redeSocialDto.Id);

                        redeSocial.EventoId = _eventoId;

                        mapper.Map(redeSocial, redeSocialDto);

                        string json = JsonSerializer.Serialize(redeSocial);

                        Console.WriteLine(json);

                        redeSocialPersistence.Update(redeSocial);

                        await redeSocialPersistence.SaveChangesAsync();
                    }
                }

                var redeSocialRetorno = await redeSocialPersistence.GetTodasRedesSociaisPorEventoIdAsync(_eventoId);
                var redeSocialDtoRetorno = mapper.Map<RedeSocialDto[]>(redeSocialRetorno);
                return redeSocialDtoRetorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RedeSocialDto[]> SavePorPalestrante(int _palestranteId, RedeSocialDto[] _redesSociaisDto)
        {
            try
            {
                Console.WriteLine("............Service2 ...... " + _redesSociaisDto[0].Id);
                var redesSociais = await redeSocialPersistence.GetTodasRedesSociaisPorPalestranteIdAsync(_palestranteId);

                if (redesSociais == null) return null;

                foreach (var redeSocialDto in _redesSociaisDto)
                {
                    if (redeSocialDto.Id == 0)
                    {
                        await AddRedeSocial(_palestranteId, redeSocialDto, false);

                    }
                    else
                    {
                        var redeSocial = redesSociais.FirstOrDefault(redeSocial => redeSocial.Id == redeSocialDto.Id);

                        redeSocial.PalestranteId = _palestranteId;

                        mapper.Map(redeSocial, redeSocialDto);

                        string json = JsonSerializer.Serialize(redeSocial);

                        Console.WriteLine(json);

                        redeSocialPersistence.Update(redeSocial);

                        await redeSocialPersistence.SaveChangesAsync();
                    }
                }

                var redeSocialRetorno = await redeSocialPersistence.GetTodasRedesSociaisPorPalestranteIdAsync(_palestranteId);
                var redeSocialDtoRetorno = mapper.Map<RedeSocialDto[]>(redeSocialRetorno);
                return redeSocialDtoRetorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
