﻿using ProEventos.Domain.Eventos;
using ProEventos.Domain.RedesSociais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Domain.Palestrantes
{
    public class PalestranteEvento
    {
        public int PalestranteId { get; set; }
        public Palestrante? Palestrante { get; set; }
        public int EventoId { get; set; }
        public Evento? Evento { get; set; }
    }
}
