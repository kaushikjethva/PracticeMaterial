using Microsoft.EntityFrameworkCore;
using EventAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventAPI.Infrastructure;

namespace EventAPI.Repositories
{
    public class EventRepository<T> : IEventRepository<T> where T : BaseEntity
    {
        private EventDBContext dBContext;
        private DbSet<T> entities;
        public EventRepository(EventDBContext db)
        {
            dBContext = db;
            entities = dBContext.Set<T>();
        }
        public async Task<T> AddAsync(T item)
        {
            await entities.AddAsync(item);
            await dBContext.SaveChangesAsync();
            return item;
        }

        public async Task<T> UpdateAsync(int id, T item)
        {
            entities.Update(item).State = EntityState.Modified;
            await dBContext.SaveChangesAsync();            
            return item;            
        }

        public async Task<T> DeleteAsync(int id)
        {
            var item = await entities.FindAsync(id);
            if (item == null)
                throw new Exception("Item not found for Update");

            entities.Remove(item);
            await dBContext.SaveChangesAsync();
            return item;
        }

        public T Get(int id)
        {
            var item = entities.Find(id);
            return item;
        }

        public IEnumerable<T> GetAll()
        {
            return entities.ToList();
        }
    }
}
