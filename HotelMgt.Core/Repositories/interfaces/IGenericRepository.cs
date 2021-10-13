using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Core.interfaces
{
    public interface IGenericRepository<TEntity>
    {
        Task<TEntity> GetAsync(string id);
        TEntity Get(string id);
        IEnumerable<TEntity> GetAll();
        TEntity Add(TEntity entity);
        Task AddAsync(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(TEntity entity);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        TEntity Update(TEntity entity);
        void SaveChanges();
    }
}
