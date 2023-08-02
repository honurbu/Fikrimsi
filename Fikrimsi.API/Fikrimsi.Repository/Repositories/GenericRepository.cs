using Fikrimsi.Core.Repositories;
using Fikrimsi.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Fikrimsi.Repository.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {

        protected readonly AppDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        public async Task AddAsync(TEntity t)
        {
            await _dbSet.AddAsync(t);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
             

        }

        public IQueryable<TEntity> GetListByFilter(Expression<Func<TEntity, bool>> expression)
        {
            return _dbSet.Where(expression);
        }

        public void Remove(TEntity t)
        {
            _dbSet.Remove(t);
        }

        public void Update(TEntity t)
        {
           _dbSet.Update(t);
        }

    }
}
