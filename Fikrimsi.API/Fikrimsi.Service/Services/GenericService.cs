using Fikrimsi.Core.Repositories;
using Fikrimsi.Core.Services;
using Fikrimsi.Core.UnitOfWork;
using Fikrimsi.Service.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Fikrimsi.Service.Services
{
    public class GenericService<TEntity> : IGenericService<TEntity> where TEntity : class
    {

        protected readonly IGenericRepository<TEntity> _genericRepository;
        protected readonly IUnitOfWork _unitOfWork;

        public GenericService(IGenericRepository<TEntity> genericRepository, IUnitOfWork unitOfWork)
        {
            _genericRepository = genericRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(TEntity t)
        {
            await _genericRepository.AddAsync(t);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _genericRepository.GetAllAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _genericRepository.GetByIdAsync(id);
        }

        public IQueryable<TEntity> GetListByFilter(Expression<Func<TEntity, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async void Remove(TEntity t)
        {
            _genericRepository.Remove(t);
            await _unitOfWork.CommitAsync();
        }

        public async void Update(TEntity t)
        {
            _genericRepository.Update(t);
            await _unitOfWork.CommitAsync();
        }

     
    }
}

    




