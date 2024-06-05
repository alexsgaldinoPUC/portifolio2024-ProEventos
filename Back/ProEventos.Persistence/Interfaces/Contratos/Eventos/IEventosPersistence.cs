using ProEventos.Domain.Models.Eventos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Interfaces.Contratos.Eventos
{
    public interface IEventosPersistence
    {
        Task<Evento[]> GetTodosEventosPorTemaAsync(string _tema, bool _incluirPalestrantes = false);
        Task<Evento[]> GetTodosEventosAsync(bool _incluirPalestrantes = false);
        Task<Evento> GetEventoPorIdAsync(int _eventoId, bool _incluirPalestrantes = false);
    }
}
