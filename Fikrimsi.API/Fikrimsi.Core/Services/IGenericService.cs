using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Fikrimsi.Core.Services
{
    public interface IGenericService<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity t);

        Task<IEnumerable<TEntity>> GetAllAsync();
        IQueryable<TEntity> GetListByFilter(Expression<Func<TEntity, bool>> expression);

        Task<TEntity> GetByIdAsync(int id);

        void Remove(TEntity t);

        void Update(TEntity t);
    }
}
