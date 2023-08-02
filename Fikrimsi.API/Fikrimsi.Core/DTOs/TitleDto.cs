using Fikrimsi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fikrimsi.Core.DTOs
{
    public class TitleDto : BaseEntity,IEntity
    {
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
