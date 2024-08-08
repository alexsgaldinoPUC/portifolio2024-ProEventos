using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProEventos.API.Util.Extensions;
using ProEventos.Application.Dtos.Usuarios;
using ProEventos.Application.Servicos.Contratos.Usuarios;
using System.Security.Claims;

namespace ProEventos.API.Controllers.Usuarios
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountServices accountServices;
        private readonly ITokenServices tokenServices;

        public AccountsController(IAccountServices _accountServices, ITokenServices _tokenServices)
        {
            accountServices = _accountServices;
            tokenServices = _tokenServices;
        }

        [HttpGet("GetUsuario")]
        public async Task<IActionResult> GetUsusario()
        {
            try
            {
                Console.WriteLine("-----------------------------------------------");
                var userName = User.GetUserName();
                Console.WriteLine(userName);
                var usuario = await accountServices.GetUsuarioByUserName(userName);
                return Ok(usuario);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Falha ao recuperar a conta, Erro: {ex.Message}");
            }
        }

        [HttpPost("CadastrarConta")]
        [AllowAnonymous]
        public async Task<IActionResult> CadastrarConta(UsuarioDto usuarioDto)
        {
            try
            {
                if (await accountServices.UsuarioExists(usuarioDto.UserName)) return BadRequest("Conta já cadastrada.");

                var usuario = await accountServices.CreateAccount(usuarioDto);

                if (usuario != null) return Ok(usuario);

                return BadRequest("Falha ao criar conta!");
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Falha ao criar a conta, Erro: {ex.Message}");
            }
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UsuarioLoginDto usuarioLoginDto)
        {
            try
            {
                var usuario = await accountServices.GetUsuarioByUserName(usuarioLoginDto.UserName);

                if (usuario == null) return Unauthorized("Usuário ou senha inválido.");

                var checkPassword = await accountServices.CheckUsuarioPasswordAsync(usuario, usuarioLoginDto.Password);

                if (!checkPassword.Succeeded) return Unauthorized();

                return Ok(new
                {
                    userName = usuario.UserName,
                    PrimeiroNome = usuario.PrimeiroNome,
                    token = tokenServices.CreateToken(usuario).Result
                });
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Falha ao realizar login com a conta, Erro: {ex.Message}");
            }
        }

        [HttpPut("AlterarConta")]
        [AllowAnonymous]
        public async Task<IActionResult> AlterarConta(UsuarioUpdateDto usuarioUpdateDto)
        {
            try
            {
                var usuario = await accountServices.GetUsuarioByUserName(User.GetUserName());
                if (usuario == null) return Unauthorized("Usuário Inváido.");

                var usuarioUpdated = await accountServices.UpdateAccount(usuarioUpdateDto);

                if (usuarioUpdated == null) return NoContent();

                return Ok(usuarioUpdated);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Falha ao atualizar a conta, Erro: {ex.Message}");
            }
        }
    }
}
