using Fikrimsi.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fikrimsi.Core.Services
{
    public interface IAuthenticationService
    {
        Task<TokenDto>CreateTokenAsync(LoginDto loginDto);
        Task<TokenDto>CreateTokenByRefreshTokenAsync(string refreshToken);
        Task<NoDataDto>RevokeByRefreshTokenAsync(string refreshToken);
        Task<ClientTokenDto> CreateTokenByClient(ClientTokenDto clientTokenDto);

    }
}
