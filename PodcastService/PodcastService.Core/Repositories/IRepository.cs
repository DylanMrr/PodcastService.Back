using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PodcastService.Core.Repositories
{
    public interface IRepository<T> where T: class
    {
        Task<T> GetAsync(int id); // получение одного объекта по id
        Task<EntityEntry<T>> AddAsync(T item); // создание объекта
        Task UpdateAsync(T item); // обновление объекта
        Task DeleteAsync(int id); // удаление объекта по id
        Task SaveAsync();  // сохранение изменений
    }
}
