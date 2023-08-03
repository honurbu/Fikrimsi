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
    public class CommentService : GenericService<Comment, CommentDto>, ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        public CommentService(IGenericRepository<Comment> genericRepository, IUnitOfWork unitOfWork, ICommentRepository commentRepository) : base(genericRepository, unitOfWork)
        {
            _commentRepository = commentRepository;
        }

        public async Task<IEnumerable<Comment>> GetCommentByTitle(int id)
        {
            return await _commentRepository.GetCommentByTitle(id);
        }
    }
}
