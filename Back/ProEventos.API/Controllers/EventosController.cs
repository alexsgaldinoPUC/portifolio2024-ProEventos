using Microsoft.AspNetCore.Mvc;
using ProEventos.API.Util.Services.Contratos.Uploads;
using ProEventos.Application.Dtos.Eventos;
using ProEventos.Application.Servicos.Contratos.Eventos;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly IEventoServices eventosServices;
        private readonly IUploadServices uploadServices;
        private readonly string destinoImagens = "Imagens";

        public EventosController(IEventoServices _eventosServices, IUploadServices _uploadServices)
        {
            eventosServices = _eventosServices;
            uploadServices = _uploadServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodosEventos()
        {
            try
            {
                var eventos = await eventosServices.GetTodosEventosAsync(true);

                if (eventos == null) return NoContent();

                return Ok(eventos);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar eventos. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventosPorId(int id)
        {
            try
            {
                var evento = await eventosServices.GetEventoPorIdAsync(id, true);

                if (evento == null) return NoContent();

                return Ok(evento);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar evento por id. Erro: {ex.Message}");
            }
        }

        [HttpGet("{tema}/tema")]
        public async Task<IActionResult> GetTodosEventosPorTema(string tema)
        {
            try
            {
                var eventos = await eventosServices.GetTodosEventosPorTemaAsync(tema, true);

                if (eventos == null) return NoContent();

                return Ok(eventos);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar evento por tema. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CriarEvento(EventoDto eventoModel)
        {
            try
            {
                var evento = await eventosServices.AddEventos(eventoModel);

                if (evento == null) return NoContent();

                return Ok(evento);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao criar evento. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AlterarEvento(int id, EventoDto eventoModel)
        {
            try
            {
                var evento = await eventosServices.UpdateEvento(id, eventoModel);

                if (evento == null) return NoContent();

                return Ok(evento);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao alterar evento. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirEvento(int id)
        {
            try
            {
                var evento = await eventosServices.GetEventoPorIdAsync(id, true);

                if (evento == null) return NoContent();

                if (await eventosServices.DeleteEvento(id))
                {
                    uploadServices.DeleteImagem(evento.ImagemUrl, destinoImagens);
                    return Ok(new { message = "Excluido" });
                } else
                {
                    throw new Exception("Ocorreu um problema inesperado ao deletar Evento."); 
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao excluir evento. Erro: {ex.Message}");
            }
        }
    }
}
