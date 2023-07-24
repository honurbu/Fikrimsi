﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fikrimsi.Core.Entities
{
    public class UserApp : IdentityUser, IEntity
    {
        public string City { get; set; }
        //public string About { get; set; }
    }
}