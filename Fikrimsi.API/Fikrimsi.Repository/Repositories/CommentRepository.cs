using Fikrimsi.Core.Entities;
using Fikrimsi.Core.Repositories;
using Fikrimsi.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fikrimsi.Repository.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
