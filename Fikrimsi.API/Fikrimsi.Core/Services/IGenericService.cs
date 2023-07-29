using Fikrimsi.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Fikrimsi.Core.Services
{
    public interface IGenericService<TEntity,TDto> where TEntity : class where TDto: class
    {
        Task AddAsync(TDto t);

        Task<IEnumerable<TDto>> GetAllAsync();
        IQueryable<TDto> GetListByFilter(Expression<Func<TEntity, bool>> expression);

        Task<TDto> GetByIdAsync(int id);

        void Remove(int id);

        void Update(TDto entity, int id);
    }
}
