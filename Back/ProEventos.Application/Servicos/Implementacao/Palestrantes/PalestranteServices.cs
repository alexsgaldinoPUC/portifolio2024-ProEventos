using AutoMapper;
using ProEventos.Application.Dtos.Palestrantes;
using ProEventos.Application.Servicos.Contratos.Palestrantes;
using ProEventos.Domain.Models.Palestrantes;
using ProEventos.Global.Models.Paginator;
using ProEventos.Persistence.Interfaces.Contratos.Palestrantes;

namespace ProEventos.Application.Servicos.Implementacao.Palestrantes
{
    public class PalestranteServices : IPalestranteServices
    {
        private readonly IPalestrantePersistence palestrantePersistence;
        private readonly IMapper mapper;

        public PalestranteServices(IPalestrantePersistence _palestrantePersistence, IMapper _mapper)
        {
            palestrantePersistence = _palestrantePersistence;
            mapper = _mapper;
        }
        public async Task<PalestranteAddDto> AddPalestrantes(int _userId, PalestranteAddDto _palestranteAddDto)
        {
            try
            {
                var palestrante = mapper.Map<Palestrante>(_palestranteAddDto);

                palestrante.UserId = _userId;

                palestrantePersistence.Add<Palestrante>(palestrante);

                if (await palestrantePersistence.SaveChangesAsync())
                {
                    var palestranteRetorno = await palestrantePersistence.GetPalestrantePorUserIdAsync(_userId, false);
                    var palestranteDtoRetorno = mapper.Map<PalestranteAddDto>(palestranteRetorno);
                    return palestranteDtoRetorno;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PalestranteDto> GetPalestrantePorUserIdAsync(int _userId, bool _incluirEventos = false)
        {
            try
            {
                var palestrante = await palestrantePersistence.GetPalestrantePorUserIdAsync(_userId, _incluirEventos);

                if (palestrante == null) return null;

                var palestranteDto = mapper.Map<PalestranteDto>(palestrante);

                return palestranteDto;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<PageList<PalestranteDto>> GetTodosPalestrantesAsync(PageParams _pageParams, bool _incluirEventos = false)
        {
            try
            {
                var palestrantes = await palestrantePersistence.GetTodosPalestrantesAsync(_pageParams, _incluirEventos);

                if (palestrantes == null) return null;

                var palestrantesDto = mapper.Map<PageList<PalestranteDto>>(palestrantes);

                palestrantesDto.CurrentPage = palestrantes.CurrentPage;
                palestrantesDto.TotalPages = palestrantes.TotalPages;
                palestrantesDto.PageSize = palestrantes.PageSize;
                palestrantesDto.TotalCount = palestrantes.TotalCount;

                return palestrantesDto;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<PalestranteUpdateDto> UpdatePalestrante(int userId, PalestranteUpdateDto _palestranteUpdateDto)
        {
            try
            {
                var palestrante = await palestrantePersistence.GetPalestrantePorUserIdAsync(userId, false);

                if (palestrante == null) return null;

                _palestranteUpdateDto.Id = palestrante.Id;
                _palestranteUpdateDto.UserId = userId;

                mapper.Map(_palestranteUpdateDto, palestrante);

                palestrantePersistence.Update(palestrante);

                if (await palestrantePersistence.SaveChangesAsync())
                {
                    var palestranteRetorno = await palestrantePersistence.GetPalestrantePorUserIdAsync(userId, false);
                    var palestranteDtoRetorno = mapper.Map<PalestranteUpdateDto>(palestranteRetorno);
                    return palestranteDtoRetorno;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
