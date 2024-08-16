using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProEventos.API.Util.Extensions;
using ProEventos.API.Util.Services.Contratos.Uploads;
using ProEventos.Application.Servicos.Contratos.Eventos;
using ProEventos.Application.Servicos.Contratos.Palestrantes;
using ProEventos.Application.Servicos.Contratos.Usuarios;

namespace ProEventos.API.Controllers.Uploads
{

    [ApiController]
    [Route("api/[controller]")]
    public class UploadsController : ControllerBase
    {
        private readonly IUploadServices uploadServices;
        private readonly IEventoServices eventoServices;
        private readonly IAccountServices accountServices;
        private readonly string destinoImagens = "Imagens";
        private readonly string destinoPerfil = "Perfil";

        public UploadsController(IUploadServices _uploadService, IEventoServices _eventoServices, IAccountServices _accountServices)
        {
            uploadServices = _uploadService;
            eventoServices = _eventoServices;
            accountServices = _accountServices;
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

                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao realizar upload de foto do evento. Erro: {ex.Message}");
            }
        }

        [HttpPost("upload-perfil")]
        public async Task<IActionResult> UploadPerfil()
        {
            try
            {
                Console.WriteLine("___________________________upload-photo-----------------------");
                var usuario = await accountServices.GetUsuarioByUserName(User.GetUserName());
                Console.WriteLine("////aqui////");
                if (usuario == null) return NoContent();

                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    uploadServices.DeleteImagem(usuario.ImagemURL, destinoPerfil);
                    usuario.ImagemURL = await uploadServices.SaveImagem(usuario.Id, file, destinoPerfil);
                }

                var usuarioUpdated = await accountServices.UpdateAccount(usuario);

                return Ok(usuarioUpdated);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao realizar upload de foto do usuário. Erro: {ex.Message}");
            }
        }

    }
}
