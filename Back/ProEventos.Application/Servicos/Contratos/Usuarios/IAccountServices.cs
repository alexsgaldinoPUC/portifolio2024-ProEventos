using Microsoft.AspNetCore.Identity;
using ProEventos.Application.Dtos.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Application.Servicos.Contratos.Usuarios
{
    public interface IAccountServices
    {
        Task<bool> UsuarioExists(string userName);
        Task<UsuarioUpdateDto> GetUsuarioByUserName(string userName);
        Task<SignInResult> CheckUsuarioPasswordAsync(UsuarioUpdateDto usuarioUpdateDto, string password);
        Task<UsuarioDto> CreateAccount(UsuarioDto usuarioDto);
        Task<UsuarioUpdateDto> UpdateAccount(UsuarioUpdateDto usuarioUpdateDto);
    }
}
