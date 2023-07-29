using Fikrimsi.Core.DTOs;
using Fikrimsi.Core.Entities;
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
    public class GenericService<TEntity,TDto> : IGenericService<TEntity,TDto> where TEntity : class where TDto : class
    {

        private readonly IGenericRepository<TEntity> _genericRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GenericService(IGenericRepository<TEntity> genericRepository, IUnitOfWork unitOfWork)
        {
            _genericRepository = genericRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(TDto t)
        {
            var newEntity = ObjectMapper.Mapper.Map<TEntity>(t);
            await _genericRepository.AddAsync(newEntity);
            await _unitOfWork.CommitAsync();

            var newDto = ObjectMapper.Mapper.Map<TDto>(newEntity);
        }

        public async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var entities = await _genericRepository.GetAllAsync();
            var dtos = ObjectMapper.Mapper.Map<IEnumerable<TDto>>(entities);
            return dtos;
        }

        public async Task<TDto> GetByIdAsync(int id)
        {
            var entity = await _genericRepository.GetByIdAsync(id);

            if (entity == null)
            {
                return null;
            }

            var dto = ObjectMapper.Mapper.Map<TDto>(entity);
            return dto;

        }

        public IQueryable<TDto> GetListByFilter(Expression<Func<TEntity, bool>> expression)
        {
            var filteredEntities = _genericRepository.GetListByFilter(expression);
            var dtos = filteredEntities.Select(entity => ObjectMapper.Mapper.Map<TDto>(entity));
            return dtos.AsQueryable();
        }

        public async void Remove(int id)
        {
            var IsExistEntity = await _genericRepository.GetByIdAsync(id);

            if (IsExistEntity == null)
            {
                throw new Exception("Id not found");
            }

            _genericRepository.Remove(IsExistEntity);

            await _unitOfWork.CommitAsync();
        }

        public async void Update(TDto entity, int id)
        {
            var IsExistEntity = await _genericRepository.GetByIdAsync(id);

            if (IsExistEntity == null)
            {
                throw new Exception("Id not found");
            }

            var updateEntity = ObjectMapper.Mapper.Map<TEntity>(entity);
            _genericRepository.Update(updateEntity);
            await _unitOfWork.CommitAsync();             
        }
    }
}

    




