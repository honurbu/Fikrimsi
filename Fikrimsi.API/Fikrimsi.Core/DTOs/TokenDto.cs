using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fikrimsi.Core.DTOs
{
    public class TokenDto       //Token içerisinde erişim kodu erişim ömrü refresh tokeni ve onun ömrü bulunur.
    {
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpiration { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }

    }
}
