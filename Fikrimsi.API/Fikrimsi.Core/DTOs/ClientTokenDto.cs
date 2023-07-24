using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fikrimsi.Core.DTOs
{
    public class ClientTokenDto     //clientlere istek yapan tokende yalnızca acces token ve ömrü olması yeterli.
    {
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpiration { get; set; }
    }
}
