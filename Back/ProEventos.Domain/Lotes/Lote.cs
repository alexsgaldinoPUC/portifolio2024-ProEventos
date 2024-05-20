using ProEventos.Domain.Eventos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Domain.Lotes
{
    public class Lote
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public decimal? Preco { get; set; }
        public DateTime? DataInicioLote { get; set;}
        public DateTime? DataFimLote { get; set; }
        public int? Quantidade { get; set;}
        public int? EventoId { get; set;}
        public Evento? Evento { get;}
    }
}
