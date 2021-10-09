using HotelMgt.Core.interfaces;
using HotelMgt.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HotelMgt.Core
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly HotelMgtDbContext _dbContext;
        public Repository(HotelMgtDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
        }


        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Where(predicate);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().ToList();
        }

        public virtual async Task<TEntity> GetAsync(string id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public virtual void Remove(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }
    }
}
