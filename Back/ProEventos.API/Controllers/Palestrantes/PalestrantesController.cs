using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProEventos.API.Util.Extensions;
using ProEventos.API.Util.Services.Contratos.Uploads;
using ProEventos.Application.Dtos.Eventos;
using ProEventos.Application.Dtos.Palestrantes;
using ProEventos.Application.Servicos.Contratos.Eventos;
using ProEventos.Application.Servicos.Contratos.Palestrantes;
using ProEventos.Application.Servicos.Contratos.Usuarios;
using ProEventos.Global.Models.Paginator;

namespace ProEventos.API.Controllers.Palestrantes
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PalestrantesController : ControllerBase
    {
        private readonly IPalestranteServices palestranteServices;
        private readonly IAccountServices accountServices;

        public PalestrantesController(IPalestranteServices _palestranteServices, IAccountServices _accountServices)
        {
            palestranteServices = _palestranteServices;
            accountServices = _accountServices;
        }

        [HttpGet("todosPalestrantes")]
        public async Task<IActionResult> GetTodosPalestrantes([FromQuery] PageParams _pageParams)
        {
            try
            {
                var palestrantes = await palestranteServices.GetTodosPalestrantesAsync(_pageParams, true);

                if (palestrantes == null) return NoContent();

                Response.AddPagination(palestrantes.CurrentPage, palestrantes.PageSize, palestrantes.TotalCount, palestrantes.TotalPages);

                return Ok(palestrantes);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar palestrantes. Erro: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetPalestrante()
        {
            try
            {
                var palestrante = await palestranteServices.GetPalestrantePorUserIdAsync(User.GetUserId(), true);

                if (palestrante == null) return NoContent();

                return Ok(palestrante);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar palestrante por userId. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CriarPalestrante(PalestranteAddDto palestranteModel)
        {
            try
            {

                var palestrante = await palestranteServices.GetPalestrantePorUserIdAsync(User.GetUserId(), false);

                if (palestrante == null) 
                     await palestranteServices.AddPalestrantes(User.GetUserId(), palestranteModel);


                return Ok(palestrante);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao criar palestrante. Erro: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> AlterarPalestrante(PalestranteUpdateDto palestranteModel)
        {
            try
            {
                var palestrante = await palestranteServices.UpdatePalestrante(User.GetUserId(), palestranteModel);

                if (palestrante == null) return NoContent();

                return Ok(palestrante);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao alterar palestrante. Erro: {ex.Message}");
            }
        }

    }
}
