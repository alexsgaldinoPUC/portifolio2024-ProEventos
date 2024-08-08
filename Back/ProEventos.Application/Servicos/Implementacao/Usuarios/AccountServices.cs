using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProEventos.Application.Dtos.Usuarios;
using ProEventos.Application.Servicos.Contratos.Usuarios;
using ProEventos.Domain.Models.Usuarios;
using ProEventos.Persistence.Interfaces.Contratos.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Application.Servicos.Implementacao.Usuarios
{
    public class AccountServices : IAccountServices
    {
        private readonly UserManager<Usuario> userManager;
        private readonly SignInManager<Usuario> signInManager;
        private readonly IMapper mapper;
        private readonly IUsuarioPersistence usuarioPersistence;

        public AccountServices(
            UserManager<Usuario> _userManager,
            SignInManager<Usuario> _signInManager,
            IMapper _mapper,
            IUsuarioPersistence _usuarioPersistence
            )
        {
            userManager = _userManager;
            signInManager = _signInManager;
            mapper = _mapper;
            usuarioPersistence = _usuarioPersistence;
        }
        public async Task<SignInResult> CheckUsuarioPasswordAsync(UsuarioUpdateDto usuarioUpdateDto, string password)
        {
            try
            {
                var usuario = await userManager.Users.SingleOrDefaultAsync(usuario => usuario.UserName == usuarioUpdateDto.UserName.ToLower());

                return await signInManager.CheckPasswordSignInAsync(usuario, password, false);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao verificar a senha. Erro: {ex.Message}");
            }
        }

        public async Task<UsuarioDto> CreateAccount(UsuarioDto usuarioDto)
        {
            try
            {
                var usuario = mapper.Map<Usuario>(usuarioDto);
                var createUsuario = await userManager.CreateAsync(usuario, usuarioDto.Password);

                if (createUsuario.Succeeded) return mapper.Map<UsuarioDto>(usuario);

                return null;
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao criar a conta. Erro: {ex.Message}");
            }
        }

        public async Task<UsuarioUpdateDto> GetUsuarioByUserName(string userName)
        {
            try
            {
                var usuario = await usuarioPersistence.getUsuarioByUserNameAsync(userName);

                if (usuario == null) return null;

                return mapper.Map<UsuarioUpdateDto>(usuario);
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao recuperar usuário pelo username. Erro: {ex.Message}");
            }
        }

        public async Task<UsuarioUpdateDto> UpdateAccount(UsuarioUpdateDto usuarioUpdateDto)
        {
            try
            {
                var usuario = await usuarioPersistence.getUsuarioByUserNameAsync(usuarioUpdateDto.UserName);

                if (usuario == null) return null;

                mapper.Map(usuarioUpdateDto, usuario);

                var token = await userManager.GeneratePasswordResetTokenAsync(usuario);
                var resetPassword = await userManager.ResetPasswordAsync(usuario, token, usuarioUpdateDto.Password);

                usuarioPersistence.Update<Usuario>(usuario);

                if (await usuarioPersistence.SaveChangesAsync())
                {
                    var usuarioAlterado = await usuarioPersistence.getUsuarioByUserNameAsync(usuario.UserName);

                    return mapper.Map<UsuarioUpdateDto>(usuarioAlterado);
                }

                return null;
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao alterar a conta. Erro: {ex.Message}");
            }
        }

        public async Task<bool> UsuarioExists(string userName)
        {
            try
            {
                return await userManager.Users.AnyAsync(usuario => usuario.UserName == userName.ToLower());
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao verificar se a conta existe. Erro: {ex.Message}");
            }
        }
    }
}
