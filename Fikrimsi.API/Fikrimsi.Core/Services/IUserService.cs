using Fikrimsi.Core.DTOs;
using Fikrimsi.SharedLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fikrimsi.Core.Services
{
    public interface IUserService
    {
        Task<Response<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto);

        Task<Response<UserAppDto>> GetUserByName(string userName);
    }
}
