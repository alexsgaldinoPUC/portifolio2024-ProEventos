using ProEventos.Domain.Models.Eventos;
using ProEventos.Domain.Models.Lotes;
using ProEventos.Persistence.Interfaces.Contratos.Geral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Interfaces.Contratos.Lotes
{
    public interface ILotePersistence : IGeralPersistence
    {
        /// <summary>
        /// Retorna os lotes de um evento
        /// </summary>
        /// <param name="_eventoId">Identificador do evento<param>
        /// <returns>Lista de Lotes</returns>
        Task<Lote[]> GetLotesPorEventoIdAsync(int _eventoId);
        /// <summary>
        /// Retorna um lote a partir de um evento
        /// </summary>
        /// <param name="_eventoId">Identificador do evento</param>
        /// <param name="_loteId">Identificador do lote</param>
        /// <returns>1 lote</returns>
        Task<Lote> GetLotePorIdsAsync(int _eventoId, int _loteId);
    }
}
