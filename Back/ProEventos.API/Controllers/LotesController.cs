using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Dtos.Lotes;
using ProEventos.Application.Servicos.Contratos.Lotes;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LotesController : ControllerBase
    {
        private readonly ILoteServices loteServices;

        public LotesController(ILoteServices _loteServices)
        {
            loteServices = _loteServices;
        }

        [HttpGet("{eventoId}")]
        public async Task<IActionResult> GetLotesByEventoId(int eventoId)
        {
            try
            {
                var eventos = await loteServices.GetLotesPorEventoIdAsync(eventoId);

                if (eventos == null) return NoContent();

                return Ok(eventos);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar Lotes. Erro: {ex.Message}");
            }
        }

        [HttpPut("{eventoId}")]
        public async Task<IActionResult> AlterarLote(int eventoId, LoteDto[] _lotesModel)
        {
            try
            {
                var evento = await loteServices.SaveLotes(eventoId, _lotesModel);

                if (evento == null) return NoContent();

                return Ok(evento);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao alterar Lote. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{eventoId}/{loteId}")]
        public async Task<IActionResult> ExcluirLote(int eventoId, int loteId)
        {
            try
            {
                var lote = await loteServices.GetLotePorIdsAsync(eventoId, loteId);

                if (lote == null) return NoContent();

                return await loteServices.DeleteLote(eventoId, loteId) ? Ok(new { message = "Excluido" }) : throw new Exception("Ocorreu um problema inesperado ao deletar Lote."); 
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao excluir Lote. Erro: {ex.Message}");
            }
        }
    }
}
