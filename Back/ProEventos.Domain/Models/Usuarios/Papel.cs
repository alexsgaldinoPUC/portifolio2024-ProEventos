using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Domain.Models.Usuarios
{
    public class Papel : IdentityRole<int>
    {
        public IEnumerable<UsuarioPapel> UsuarioPapeis { get; set; }
    }
}
