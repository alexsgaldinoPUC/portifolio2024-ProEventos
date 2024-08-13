using ProEventos.Domain.Models.Eventos;
using ProEventos.Global.Models.Paginator;
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
        Task<PageList<Evento>> GetTodosEventosAsync(int _userId, PageParams _pageParams, bool _incluirPalestrantes = false);
        Task<Evento> GetEventoPorIdAsync(int _userId, int _eventoId, bool _incluirPalestrantes = false);
    }
}
