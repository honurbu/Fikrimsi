using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Fikrimsi.Core.Entities
{
    public class UserApp : IdentityUser, IEntity
    {

        //Relations

        [JsonIgnore]
        public List<Comment> Comments { get; set; }

    }
}
