using Fikrimsi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Fikrimsi.Core.DTOs
{
    public class CommentDto
    {
        public string Content { get; set; }

        public DateTime CommentDate { get; set; }


        //Navigations

        //[JsonIgnore]
        public int TitleId { get; set; }


        //[JsonIgnore]
        public string UserAppId { get; set; }
    }
}
