using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProEventos.API.Util.Extensions;
using ProEventos.API.Util.Services.Contratos.Uploads;
using ProEventos.Application.Dtos.Eventos;
using ProEventos.Application.Servicos.Contratos.Eventos;
using ProEventos.Application.Servicos.Contratos.Usuarios;

namespace ProEventos.API.Controllers.Eventos
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly IEventoServices eventosServices;
        private readonly IUploadServices uploadServices;
        private readonly IAccountServices accountServices;
        private readonly string destinoImagens = "Imagens";

        public EventosController(IEventoServices _eventosServices, IUploadServices _uploadServices, IAccountServices _accountServices)
        {
            eventosServices = _eventosServices;
            uploadServices = _uploadServices;
            accountServices = _accountServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodosEventos()
        {
            try
            {
                var eventos = await eventosServices.GetTodosEventosAsync(User.GetUserId(), true);

                if (eventos == null) return NoContent();

                return Ok(eventos);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar eventos. Erro: {ex.Message}");
            }
        }

        [HttpGet("{eventoId}")]
        public async Task<IActionResult> GetEventosPorId(int eventoId)
        {
            try
            {
                var evento = await eventosServices.GetEventoPorIdAsync(User.GetUserId(), eventoId, true);

                if (evento == null) return NoContent();

                return Ok(evento);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar evento por id. Erro: {ex.Message}");
            }
        }

        [HttpGet("{tema}/tema")]
        public async Task<IActionResult> GetTodosEventosPorTema(string tema)
        {
            try
            {
                var eventos = await eventosServices.GetTodosEventosPorTemaAsync(User.GetUserId(), tema, true);

                if (eventos == null) return NoContent();

                return Ok(eventos);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar evento por tema. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CriarEvento(EventoDto eventoModel)
        {
            try
            {
                var evento = await eventosServices.AddEventos(User.GetUserId(), eventoModel);

                if (evento == null) return NoContent();

                return Ok(evento);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao criar evento. Erro: {ex.Message}");
            }
        }

        [HttpPut("{eventoId}")]
        public async Task<IActionResult> AlterarEvento(int eventoId, EventoDto eventoModel)
        {
            try
            {
                var evento = await eventosServices.UpdateEvento(User.GetUserId(), eventoId, eventoModel);

                if (evento == null) return NoContent();

                return Ok(evento);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao alterar evento. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{eventoId}")]
        public async Task<IActionResult> ExcluirEvento(int eventoId)
        {
            try
            {
                var evento = await eventosServices.GetEventoPorIdAsync(User.GetUserId(), eventoId, true);

                if (evento == null) return NoContent();

                if (await eventosServices.DeleteEvento(User.GetUserId(), eventoId))
                {
                    uploadServices.DeleteImagem(evento.ImagemUrl, destinoImagens);
                    return Ok(new { message = "Excluido" });
                }
                else
                {
                    throw new Exception("Ocorreu um problema inesperado ao deletar Evento.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao excluir evento. Erro: {ex.Message}");
            }
        }
    }
}
