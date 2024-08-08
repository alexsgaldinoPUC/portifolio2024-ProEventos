using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Models.Usuarios;
using ProEventos.Persistence.Data;
using ProEventos.Persistence.Interfaces.Contratos.Usuarios;
using ProEventos.Persistence.Interfaces.Implementacao.Geral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Interfaces.Implementacao.Usuarios
{
    public class UsuarioPersistence : GeralPersistence, IUsuarioPersistence
    {
        private readonly ProEventosContext context;

        public UsuarioPersistence(ProEventosContext _context) : base(_context)
        {
            context = _context;
        }
        public async Task<Usuario> getUsuarioByIdAsync(int userId)
        {
            return await context.Users.FindAsync(userId);
        }

        public async Task<Usuario> getUsuarioByUserNameAsync(string userName)
        {
            return await context.Users.SingleOrDefaultAsync(user => user.UserName == userName.ToLower()); 
        }

        public async Task<IEnumerable<Usuario>> getUsuariosAsync()
        {
            return await context.Users.ToListAsync();
        }
    }
}
