using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Servicos.Contratos.Eventos;
using ProEventos.Domain.Eventos;
using ProEventos.Persistence.Data;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly IEventosServices eventosServices;

        public EventosController(IEventosServices _eventosServices)
        {
            eventosServices = _eventosServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodosEventos()
        {
            try
            {
                var eventos = await eventosServices.GetTodosEventosAsync(true);

                if (eventos == null) return NotFound("Nenhum evento encontrado!");

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

                if (evento == null) return NotFound($"Evento {id} encontrado!");

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

                if (eventos == null) return NotFound($"Nenhum evento para o tema '{tema}' encontrado!");

                return Ok(eventos);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar evento por tema. Erro: {ex.Message}");
            }
        }

        [HttpPost()]
        public async Task<IActionResult> CriarEvento(Evento eventoModel)
        {
            try
            {
                var evento = await eventosServices.AddEventos(eventoModel);

                if (evento == null) return BadRequest($"Erro ao criar evento {eventoModel.Id}!");

                return Ok(evento);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao criar evento. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AlterarEvento(int id, Evento eventoModel)
        {
            try
            {
                var evento = await eventosServices.UpdateEvento(id, eventoModel);

                if (evento == null) return BadRequest($"Erro ao alterar evento {eventoModel.Id}!");

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
                return await eventosServices.DeleteEvento(id) ? Ok("EXcluído") : BadRequest($"Erro ao excluir evento {id}!"); 
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao excluir evento. Erro: {ex.Message}");
            }
        }
    }
}
