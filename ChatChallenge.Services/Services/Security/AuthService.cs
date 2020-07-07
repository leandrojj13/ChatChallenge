using AutoMapper;
using ChatChallenge.Bl.Dto.Security;
using ChatChallenge.Core.Models;
using ChatChallenge.Model.Entities.Security;
using ChatChallenge.Services.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ChatChallenge.Services.Services.Security
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(AuthRequest loginInfo);
        string GetToken(User user);
    }

    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly TokenSetting _tokenSetting;

        public AuthService(IMapper mapper, IUserService userService, TokenSetting tokenSetting)
        {
            _userService = userService;
            _mapper = mapper;
            _tokenSetting = tokenSetting;
        }

        public async Task<AuthResponse> Login(AuthRequest loginInfo)
        {
            var result = await _userService.IsValid(loginInfo.UserName, loginInfo.Password);
            if (result.Succeeded)
            {
                var user = await _userService.GetUserByUsername(loginInfo.UserName);
                var userDto = _mapper.Map<UserDto>(user);

                var token = GetToken(user);

                return new AuthResponse
                {
                    User = userDto,
                    Token = token,
                    Description = "Ok"
                };
            }
            else
            {
                return new AuthResponse
                {
                    Description = "Incorrect user or password"
                };
            }
        }

        public string GetToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var aKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_tokenSetting.Secret));
            var claims = new[] { new Claim(ClaimTypes.NameIdentifier, user.UserName) };
            var credentials = new SigningCredentials(aKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_tokenSetting.Issuer, _tokenSetting.Audience, claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: credentials);
            return tokenHandler.WriteToken(token);
        }
    }
}
