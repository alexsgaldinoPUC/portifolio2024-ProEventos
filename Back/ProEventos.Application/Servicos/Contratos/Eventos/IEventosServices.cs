using ProEventos.Domain.Eventos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Application.Servicos.Contratos.Eventos
{
    public interface IEventosServices
    {
        Task<Evento> AddEventos(Evento _evento);
        Task<Evento> UpdateEvento(int _eventoId, Evento _evento);
        Task<bool> DeleteEvento(int _eventoId);

        Task<Evento[]> GetTodosEventosPorTemaAsync(string _tema, bool _incluirPalestrantes = false);
        Task<Evento[]> GetTodosEventosAsync(bool _incluirPalestrantes = false);
        Task<Evento> GetEventoPorIdAsync(int _eventoId, bool _incluirPalestrantes = false);    }
}
