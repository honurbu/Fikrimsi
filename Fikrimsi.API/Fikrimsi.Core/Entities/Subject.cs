using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Fikrimsi.Core.Entities
{
    public class Subject : BaseEntity,IEntity
    {
        public string Name { get; set; }




        //Relations

        [JsonIgnore]
        public List<Title> Titles { get; set; }
    }
}
