using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PodcastService.Core.Repositories
{
    public class BaseEntityFrameworkRepository<T>: IRepository<T> where T: class
    {
        protected DbContext _db;

        public BaseEntityFrameworkRepository(DbContext context)
        {
            _db = context;
        }

        public async Task<EntityEntry<T>> AddAsync(T item)
        {
            var createdItem = await _db.Set<T>().AddAsync(item);
            await _db.SaveChangesAsync();
            return createdItem;
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _db.Set<T>().FindAsync(id);
            if (item == null)
            {
                throw new Exception("Здесь нет такого");
            }
            _db.Set<T>().Remove(item);
            await _db.SaveChangesAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            return await _db.Set<T>().FindAsync(id);
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(T item)
        {
            _db.Update(item);
            await _db.SaveChangesAsync();
        }
    }
}
