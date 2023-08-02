using Fikrimsi.Core.DTOs;
using Fikrimsi.Core.Entities;
using Fikrimsi.Core.Repositories;
using Fikrimsi.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fikrimsi.Repository.Repositories
{
    public class TitleRepository : GenericRepository<Title>, ITitleRepository
    {
        public TitleRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Title>> GetAllWithSubjectsAsync()
        {
            return await _dbContext.Titles.Include(x => x.Subjects).ToListAsync();
        }
    }
}
