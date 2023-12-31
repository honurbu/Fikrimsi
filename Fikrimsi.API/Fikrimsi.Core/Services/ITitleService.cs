﻿using Fikrimsi.Core.DTOs;
using Fikrimsi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fikrimsi.Core.Services
{
    public interface ITitleService : IGenericService<Title,TitleDto>
    {
        Task<IEnumerable<Title>> GetAllWithSubjectsAsync();

    }
}
