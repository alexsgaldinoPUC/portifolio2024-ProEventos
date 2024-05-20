using ProEventos.Persistence.Data;
using ProEventos.Persistence.Interfaces.Contratos.Geral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Interfaces.Implementacao.Geral
{
    public class GeralPersistence : IGeralPersistence
    {
        private readonly ProEventosContext context;

        public GeralPersistence(ProEventosContext _context)
        {
            context = _context;
        }
        public void Add<T>(T entity) where T : class
        {
            context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            context.Remove(entity);
        }

        public void DeleteRange<T>(T[] entityArray) where T : class
        {
            context.RemoveRange(entityArray);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await  context.SaveChangesAsync() > 0);
        }

        public void Update<T>(T entity) where T : class
        {
            context.Update(entity);
        }
    }
}
