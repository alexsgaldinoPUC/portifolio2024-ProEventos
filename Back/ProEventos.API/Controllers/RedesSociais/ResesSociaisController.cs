using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProEventos.API.Util.Extensions;
using ProEventos.Application.Dtos.Lotes;
using ProEventos.Application.Dtos.RedesSociais;
using ProEventos.Application.Servicos.Contratos.Eventos;
using ProEventos.Application.Servicos.Contratos.Lotes;
using ProEventos.Application.Servicos.Contratos.Palestrantes;
using ProEventos.Application.Servicos.Contratos.RedesSociais;
using ProEventos.Application.Servicos.Implementacao.RedesSociais;

namespace ProEventos.API.Controllers.RedesSociais
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RedesSociaisController : ControllerBase
    {
        private readonly IRedeSocialServices redeSocialServices;
        private readonly IEventoServices eventoServices;
        private readonly IPalestranteServices palestranteServices;

        public RedesSociaisController(IRedeSocialServices _redeSocialServices, IEventoServices _eventoServices, IPalestranteServices _palestranteServices)
        {
            redeSocialServices = _redeSocialServices;
            eventoServices = _eventoServices;
            palestranteServices = _palestranteServices;
        }

        [HttpGet("evento/{eventoId}")]
        public async Task<IActionResult> GetRedesSociaisPorEventoId(int eventoId)
        {
            try
            {
                var evento = await eventoServices.GetEventoPorIdAsync(User.GetUserId(), eventoId, false);

                if (evento == null) return Unauthorized();

                var redeSociais = await redeSocialServices.GetTodasRedesSociaisPorEventoIdAsync(eventoId);

                if (redeSociais == null) return NoContent();

                return Ok(redeSociais);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar Lotes. Erro: {ex.Message}");
            }
        }

        [HttpGet("palestrante")]
        public async Task<IActionResult> GetRedesSociaisPorPalestranteId()
        {
            try
            {
                var palestrante = await palestranteServices.GetPalestrantePorUserIdAsync(User.GetUserId(), false);

                if (palestrante == null) return Unauthorized();

                var redeSociais = await redeSocialServices.GetTodasRedesSociaisPorPalestranteIdAsync(palestrante.Id);

                if (redeSociais == null) return NoContent();

                return Ok(redeSociais);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar Palestrante. Erro: {ex.Message}");
            }
        }

        [HttpPut("evento/{eventoId}")]
        public async Task<IActionResult> SaveRedesSociaisPorEvento(int eventoId, RedeSocialDto[] _redesSociaisModel)
        {
            try
            {
                Console.Write("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                var evento = await eventoServices.GetEventoPorIdAsync(User.GetUserId(), eventoId, false);

                if (evento == null) return Unauthorized();

                var redeSocial = await redeSocialServices.SavePorEvento(eventoId, _redesSociaisModel);

                if (redeSocial == null) return NoContent();

                return Ok(redeSocial);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao alterar Rede Social por Evento. Erro: {ex.Message}");
            }
        }

        [HttpPut("palestrante")]
        public async Task<IActionResult> SaveRedesSociaisPorPalestrante(RedeSocialDto[] _redesSociaisModel)
        {
            try
            {
                Console.Write("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
                var palestrante = await palestranteServices.GetPalestrantePorUserIdAsync(User.GetUserId(), false);

                if (palestrante == null) return Unauthorized();

                var redeSocial = await redeSocialServices.SavePorPalestrante(palestrante.Id, _redesSociaisModel);

                if (redeSocial == null) return NoContent();

                return Ok(redeSocial);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao alterar Rede Social por Palestrante. Erro: {ex.Message}");
            }
        }

        [HttpDelete("evento/{eventoId}/{redeSocialId}")]
        public async Task<IActionResult> ExcluirRedeSocialPorEventoId(int eventoId, int redeSocialId)
        {
            try
            {
                var evento = await eventoServices.GetEventoPorIdAsync(User.GetUserId(), eventoId, false);

                if (evento == null) return Unauthorized();

                var redeSocial = await redeSocialServices.GetRedeSocialEventoPorIdsAsync(eventoId, redeSocialId);

                if (redeSocial == null) return NoContent();

                return await redeSocialServices.DeletePorEvento(eventoId, redeSocialId) ? Ok(new { message = "Excluido" }) : throw new Exception("Ocorreu um problema inesperado ao deletar Rede Social por Evento.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao excluir Rede Social por Evento. Erro: {ex.Message}");
            }
        }

        [HttpDelete("palestrante/{redeSocialId}")]
        public async Task<IActionResult> ExcluirRedeSocialPorPalestranteId(int redeSocialId)
        {
            try
            {
                var palestrante = await palestranteServices.GetPalestrantePorUserIdAsync(User.GetUserId(), false);

                if (palestrante == null) return Unauthorized();

                var redeSocial = await redeSocialServices.GetRedeSocialPalestrantePorIdsAsync(palestrante.Id, redeSocialId);

                if (redeSocial == null) return NoContent();

                return await redeSocialServices.DeletePorPalestrante(palestrante.Id, redeSocialId) ? Ok(new { message = "Excluido" }) : throw new Exception("Ocorreu um problema inesperado ao deletar Rede Social por Palestrante.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao excluir Rede Social por Palestrante. Erro: {ex.Message}");
            }
        }
    }
}
