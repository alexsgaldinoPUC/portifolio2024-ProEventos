using ProEventos.Application.Dtos.Eventos;
using ProEventos.Application.Dtos.RedesSociais;
using ProEventos.Application.Dtos.Usuarios;

namespace ProEventos.Application.Dtos.Palestrantes
{
    public class PalestranteDto
    {
        public int Id { get; set; }
        public string MiniCurriculo { get; set; }
        public int UserId { get; set; }
        public UsuarioUpdateDto User { get; set; }
        public IEnumerable<RedeSocialDto> RedesSociais { get; set; }
        public IEnumerable<EventoDto> Eventos { get; set; }
    }
}
