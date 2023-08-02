using Fikrimsi.Core.DTOs;
using Fikrimsi.Core.Entities;
using Fikrimsi.Core.Repositories;
using Fikrimsi.Core.Services;
using Fikrimsi.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fikrimsi.Service.Services
{
    public class TitleService : GenericService<Title, TitleDto>, ITitleService
    {
        private readonly ITitleRepository _titleRepository;

        public TitleService(IGenericRepository<Title> genericRepository, IUnitOfWork unitOfWork, ITitleRepository titleRepository) : base(genericRepository, unitOfWork)
        {
            _titleRepository = titleRepository;
        }

        public async Task<IEnumerable<Title>> GetAllWithSubjectsAsync()
        {
            return await _titleRepository.GetAllWithSubjectsAsync();
        }
    }
}
