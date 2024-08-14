using ProEventos.Application.Dtos.Eventos;
using ProEventos.Application.Dtos.Palestrantes;

namespace ProEventos.Application.Dtos.RedesSociais
{
    public class RedeSocialDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string URL { get; set; }
        public int? EventoId { get; set; }
        public EventoDto Evento { get; set; }
        public int? palestranteId { get; set; }
        public PalestranteDto Palestrante { get; set; }
    }
}
