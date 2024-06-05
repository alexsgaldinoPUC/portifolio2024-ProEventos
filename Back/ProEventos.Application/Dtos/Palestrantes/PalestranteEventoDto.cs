using ProEventos.Application.Dtos.Eventos;

namespace ProEventos.Application.Dtos.Palestrantes
{
    public class PalestranteEventoDto
    {
        public int PalestranteId { get; set; }
        public PalestranteDto Palestrante { get; set; }
        public int EventoId { get; set; }
        public EventoDto Evento { get; set; }
    }
}
