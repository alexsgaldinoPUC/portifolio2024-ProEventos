using ProEventos.Application.Dtos.RedesSociais;

namespace ProEventos.Application.Dtos.Palestrantes
{
    public class PalestranteDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string MiniCurriculo { get; set; }
        public string ImagemUrL { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public IEnumerable<RedeSocialDto> RedesSociais { get; set; }
        public IEnumerable<PalestranteEventoDto> PalestrantesEventos { get; set; }
    }
}
