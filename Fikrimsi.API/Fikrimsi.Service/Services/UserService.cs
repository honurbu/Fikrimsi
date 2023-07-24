using AutoMapper.Internal.Mappers;
using Fikrimsi.Core.DTOs;
using Fikrimsi.Core.Entities;
using Fikrimsi.Core.Services;
using Fikrimsi.Core.UnitOfWork;
using Fikrimsi.Service.Mapping;
using Fikrimsi.SharedLibrary.Dtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fikrimsi.Service.Services
{
    public class UserService : IUserService
    {
        UserManager<UserApp> _userManager;
        private readonly IUnitOfWork _unitOfWork;


        public UserService(UserManager<UserApp> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = new UserApp
            {
                Email = createUserDto.Email,
                UserName = createUserDto.UserName,
            };

            // Password hashing
            var result = await _userManager.CreateAsync(user, createUserDto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return Response<UserAppDto>.Fail(new ErrorDto(errors, true), 400);
            }

            await _unitOfWork.CommitAsync();

            return Response<UserAppDto>.Success(ObjectMapper.Mapper.Map<UserAppDto>(user), 200);
        }

        public async Task<Response<UserAppDto>> GetUserByName(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
                return Response<UserAppDto>.Fail("User Not Found !",404,true);



            return Response<UserAppDto>.Success(ObjectMapper.Mapper.Map<UserAppDto>(user), 200);

        }
    }
}
