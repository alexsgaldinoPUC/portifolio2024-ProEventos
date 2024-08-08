using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Formatters.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProEventos.Application.Dtos.Usuarios;
using ProEventos.Application.Servicos.Contratos.Usuarios;
using ProEventos.Domain.Models.Usuarios;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Application.Servicos.Implementacao.Usuarios
{
    public class TokenServices : ITokenServices
    {
        private readonly IConfiguration config;
        private readonly UserManager<Usuario> userManager;
        private readonly IMapper mapper;
        public SymmetricSecurityKey key;

        public TokenServices(IConfiguration _config, UserManager<Usuario> _userManager, IMapper _mapper)
        {
            config = _config;
            userManager = _userManager;
            mapper = _mapper;
            key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }
        public async Task<string> CreateToken(UsuarioUpdateDto usuarioUpdateDto)
        {
            try
            {
                var usuario = mapper.Map<Usuario>(usuarioUpdateDto);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Name, usuario.UserName)
                };

                var roles = await userManager.GetRolesAsync(usuario);

                claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var tokenDescription = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = creds
                };

                var tokenHandler = new JwtSecurityTokenHandler();

                var token = tokenHandler.CreateToken(tokenDescription);

                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {

                throw new Exception($"Falha ao criar token. {ex.Message}");
            }
        }
    }
}
