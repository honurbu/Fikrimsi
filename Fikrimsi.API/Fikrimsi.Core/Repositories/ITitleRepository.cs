﻿using Fikrimsi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fikrimsi.Core.Repositories
{
    public interface ITitleRepository : IGenericRepository<Title>
    {
        Task<IEnumerable<Title>> GetAllWithSubjectsAsync();

    }
}
