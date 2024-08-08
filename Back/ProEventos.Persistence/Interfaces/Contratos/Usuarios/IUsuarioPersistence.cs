using ProEventos.Domain.Models.Usuarios;
using ProEventos.Persistence.Interfaces.Contratos.Geral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Interfaces.Contratos.Usuarios
{
    public interface IUsuarioPersistence : IGeralPersistence
    {
        Task<IEnumerable<Usuario>> getUsuariosAsync();
        Task<Usuario> getUsuarioByIdAsync(int UserId);
        Task<Usuario> getUsuarioByUserNameAsync(string userName);
    }
}
