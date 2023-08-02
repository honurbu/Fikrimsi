using Fikrimsi.Core.Configuration;
using Fikrimsi.Core.DTOs;
using Fikrimsi.Core.Entities;
using Fikrimsi.Core.Repositories;
using Fikrimsi.Core.Services;
using Fikrimsi.Core.UnitOfWork;
using Fikrimsi.Repository.Context;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fikrimsi.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly List<Client> _client;
        private readonly UserManager<UserApp> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<UserRefreshToken> _userRefreshTokenService;
        private readonly AppDbContext _appDbContext;

        public AuthenticationService(IOptions<List<Client>> client, UserManager<UserApp> userManager, ITokenService tokenService, IUnitOfWork unitOfWork, IGenericRepository<UserRefreshToken> userRefreshTokenService, RoleManager<IdentityRole> roleManager, AppDbContext appDbContext)
        {
            _client = client.Value;
            _userManager = userManager;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
            _userRefreshTokenService = userRefreshTokenService;
            _roleManager = roleManager;
            _appDbContext = appDbContext;
        }

        public async Task<TokenDto> CreateTokenAsync(LoginDto loginDto)
        {
            try
            {
                if (loginDto == null) throw new ArgumentNullException(nameof(loginDto));

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) throw new Exception("Email or Password Wrong !");

            if (!await _userManager.CheckPasswordAsync(user, loginDto.Password)) throw new Exception("Email or Password Wrong !");

            // Count the comments for the user
            int commentCount = await GetCommentCountByUserId(user.Id);

            // Check if comment count is greater than 10
            if (commentCount > 10)
            {
                // Assign the "aaa" role to the user
                await _userManager.AddToRoleAsync(user, "Yorumcu");
            }

            var token = _tokenService.CreateToken(user);


            var userRefreshToken = await _userRefreshTokenService.GetListByFilter(x => x.UserId == user.Id).SingleOrDefaultAsync();

            if (userRefreshToken == null)
            {
                await _userRefreshTokenService.AddAsync(new UserRefreshToken { UserId = user.Id, Code = token.RefreshToken, Expiration = token.RefreshTokenExpiration });
            }
            else
            {
                userRefreshToken.Code = token.RefreshToken;
                userRefreshToken.Expiration = token.RefreshTokenExpiration;
            }
           

            await _unitOfWork.CommitAsync();

            return token;
        }

        public ClientTokenDto CreateTokenByClient(ClientLoginDto clientLoginDto)
        {
            var client = _client.SingleOrDefault(x => x.Id == clientLoginDto.ClientId && x.Secret == clientLoginDto.ClientSecret);
            if (client == null) throw new Exception("ClientId or ClientSecret Not Found !");

            var token = _tokenService.CreateTokenByClient(client);
            return token;
        }

        public async Task<TokenDto> CreateTokenByRefreshTokenAsync(string refreshToken)
        {
            var existRefrehToken = await _userRefreshTokenService.GetListByFilter(x => x.Code == refreshToken).SingleOrDefaultAsync();
            if (existRefrehToken == null) throw new Exception("Refreh Token not Found !");

            var user = await _userManager.FindByIdAsync(existRefrehToken.UserId);
            if (user == null) throw new Exception("User Id not Found !");


            var tokenDto = _tokenService.CreateToken(user);
            existRefrehToken.Code = tokenDto.RefreshToken;
            existRefrehToken.Expiration = tokenDto.RefreshTokenExpiration;

            await _unitOfWork.CommitAsync();
            return tokenDto;
        }

        public async Task<NoDataDto> RevokeByRefreshTokenAsync(string refreshToken)
        {
            var existRefrehToken = await _userRefreshTokenService.GetListByFilter(x => x.Code == refreshToken).SingleOrDefaultAsync();
            if (existRefrehToken == null) throw new Exception("Refreh Token not Found !");

            _userRefreshTokenService.Remove(existRefrehToken);

            await _unitOfWork.CommitAsync();

            return new NoDataDto();

        }



        private async Task<int> GetCommentCountByUserId(string userId)
        {
            return await _appDbContext.Comments.CountAsync(c => c.UserAppId == userId);
        }

    }
}
