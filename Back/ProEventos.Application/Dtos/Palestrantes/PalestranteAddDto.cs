﻿using ProEventos.Application.Dtos.RedesSociais;

namespace ProEventos.Application.Dtos.Palestrantes
{
    public class PalestranteAddDto
    {
        public int Id { get; set; }
        public string MiniCurriculo { get; set; }
        public int UserId { get; set; }
    }
}