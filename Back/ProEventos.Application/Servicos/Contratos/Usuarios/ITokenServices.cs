using ProEventos.Application.Dtos.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Application.Servicos.Contratos.Usuarios
{
    public interface ITokenServices
    {
        Task<string> CreateToken(UsuarioUpdateDto usuarioUpdateDto);
    }
}
