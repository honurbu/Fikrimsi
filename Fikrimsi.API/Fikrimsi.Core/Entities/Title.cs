using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Fikrimsi.Core.Entities
{
    public class Title : BaseEntity,IEntity
    {
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }



        //Relations

         [JsonIgnore]
        public int SubjectId { get; set; }
        public Subject Subjects { get; set; }



        [JsonIgnore]
        public List<Comment> Comments { get; set; }



    }
}
