using Fikrimsi.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fikrimsi.Core.Services
{
    public interface IUserService
    {
        Task<UserAppDto> CreateUserAsync(CreateUserDto createUserDto);

        Task<UserAppDto> GetUserByName(string userName);
    }
}
