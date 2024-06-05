using ProEventos.Domain.Models.Eventos;
using ProEventos.Domain.Models.Palestrantes;

namespace ProEventos.Domain.Models.RedesSociais
{
    public class RedeSocial
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string URL { get; set; }
        public int EventoId { get; set; }
        public Evento Evento { get; set; }
        public int PalstranteId { get; set; }
        public Palestrante Palestrante { get; set; }
    }
}
