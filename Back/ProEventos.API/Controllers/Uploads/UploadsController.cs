using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProEventos.API.Util.Extensions;
using ProEventos.API.Util.Services.Contratos.Uploads;
using ProEventos.Application.Servicos.Contratos.Eventos;

namespace ProEventos.API.Controllers.Uploads
{

    [ApiController]
    [Route("api/[controller]")]
    public class UploadsController : ControllerBase
    {
        private readonly IUploadServices uploadServices;
        private readonly IEventoServices eventoServices;
        private readonly string destinoImagens = "Imagens";

        public UploadsController(IUploadServices _uploadService, IEventoServices _eventoServices)
        {
            uploadServices = _uploadService;
            eventoServices = _eventoServices;
        }

        [HttpPost("upload-image/{eventoId}")]
        public async Task<IActionResult> UploadImagem(int eventoId)
        {
            try
            {
                Console.WriteLine("___________________________upload-iamge_________________________________");
                var evento = await eventoServices.GetEventoPorIdAsync(User.GetUserId(), eventoId);
                Console.WriteLine("////aqui////");
                if (evento == null) return NoContent();

                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    uploadServices.DeleteImagem(evento.ImagemUrl, destinoImagens);
                    evento.ImagemUrl = await uploadServices.SaveImagem(evento.Id, file, destinoImagens);
                }

                var eventoUpdated = await eventoServices.UpdateEvento(User.GetUserId(), eventoId, evento);

                return Ok(eventoUpdated);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao criar evento. Erro: {ex.Message}");
            }
        }

    }
}
