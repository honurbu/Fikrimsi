using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fikrimsi.Core.Entities
{
    public class UserRefreshToken : IEntity
    {
        public string UserId { get; set; }
        public string Code { get; set; }   //RefreshTokenCode
        public DateTime Expiration { get; set; }
    }
}
