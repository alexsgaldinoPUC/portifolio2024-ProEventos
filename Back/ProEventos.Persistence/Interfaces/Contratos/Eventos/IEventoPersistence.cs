using ProEventos.Domain.Models.Eventos;
using ProEventos.Persistence.Interfaces.Contratos.Geral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Interfaces.Contratos.Eventos
{
    public interface IEventoPersistence : IGeralPersistence
    {
        Task<Evento[]> GetTodosEventosPorTemaAsync(int userId, string _tema, bool _incluirPalestrantes = false);
        Task<Evento[]> GetTodosEventosAsync(int userId, bool _incluirPalestrantes = false);
        Task<Evento> GetEventoPorIdAsync(int userId, int _eventoId, bool _incluirPalestrantes = false);
    }
}
