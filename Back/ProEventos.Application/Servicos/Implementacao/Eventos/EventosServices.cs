using ProEventos.Application.Servicos.Contratos.Eventos;
using ProEventos.Domain.Eventos;
using ProEventos.Persistence.Interfaces.Contratos.Eventos;
using ProEventos.Persistence.Interfaces.Contratos.Geral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Application.Servicos.Implementacao.Eventos
{
    public class EventosServices : IEventosServices
    {
        private readonly IGeralPersistence geralPersistence;
        private readonly IEventosPersistence eventosPersistence;

        public EventosServices(IGeralPersistence _geralPersistence, IEventosPersistence _eventosPersistence)
        {
            geralPersistence = _geralPersistence;
            eventosPersistence = _eventosPersistence;
        }
        public async Task<Evento> AddEventos(Evento _evento)
        {
            try
            {
                geralPersistence.Add<Evento>(_evento);

                if (await geralPersistence.SaveChangesAsync())
                {
                    return await eventosPersistence.GetEventoPorIdAsync(_evento.Id, false);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteEvento(int _eventoId)
        {
            try
            {
                var evento = await eventosPersistence.GetEventoPorIdAsync(_eventoId, false);

                if (evento == null) throw new Exception("Evento não encontrado para deleção.");

                geralPersistence.Delete<Evento>(evento);

                return await geralPersistence.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento> GetEventoPorIdAsync(int _eventoId, bool _incluirPalestrantes = false)
        {
            try
            {
                var evento = await eventosPersistence.GetEventoPorIdAsync(_eventoId, _incluirPalestrantes);

                if (evento == null) return null;

                return evento;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<Evento[]> GetTodosEventosAsync(bool _incluirPalestrantes = false)
        {
            try
            {
                var eventos = await eventosPersistence.GetTodosEventosAsync(_incluirPalestrantes);

                if (eventos == null) return null;

                return eventos;
            }
            catch(Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<Evento[]> GetTodosEventosPorTemaAsync(string _tema, bool _incluirPalestrantes = false)
        {
            try
            {
                var eventos = await eventosPersistence.GetTodosEventosPorTemaAsync(_tema, _incluirPalestrantes);

                if (eventos == null) return null;

                return eventos;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<Evento> UpdateEvento(int _eventoId, Evento _evento)
        {
            try
            {
                var evento = await eventosPersistence.GetEventoPorIdAsync(_eventoId, false);

                if (evento == null) return null;

                _evento.Id = evento.Id;

                geralPersistence.Update(_evento);

                if (await geralPersistence.SaveChangesAsync())
                {
                    return await eventosPersistence.GetEventoPorIdAsync(_evento.Id, false);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
