using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Fikrimsi.Core.Entities
{
    public class Comment : BaseEntity, IEntity
    {
        public string Content { get; set; }
        public DateTime CommentDate { get; set; }


        //Navigations

        //[JsonIgnore]
        public int TitleId { get; set; }
        public Title Title { get; set; }


        //[JsonIgnore]
        public string UserAppId { get; set; }
        public UserApp UserApp { get; set; }

    }
}
