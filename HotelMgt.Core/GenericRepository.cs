using HotelMgt.Core.interfaces;
using HotelMgt.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelMgt.Core
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly HotelMgtDbContext _dbContext;
        public GenericRepository(HotelMgtDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual TEntity Add(TEntity entity)
        {
            return _dbContext.Set<TEntity>().Add(entity).Entity;
            //return _dbContext.Add(entity).Entity;
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await _dbContext.AddAsync(entity);
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public virtual Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public virtual TEntity Get(string id)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public virtual Task<TEntity> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public virtual void Remove(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public virtual void RemoveRange(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public virtual void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public virtual TEntity Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
