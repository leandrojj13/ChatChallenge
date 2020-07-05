using ChatChallenge.Model.Contexts.ChatChallenge;
using ChatChallenge.Model.Entities.Security;
using ChatChallenge.Model.UnitOfWorks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ChatChallenge.Services.Services.Security
{
    public interface IUserService 
    {
        Task<IdentityResult> CreateUser(User user, string password);
        Task<SignInResult> IsValid(string username, string password);
        Task<User> GetUserByUsername(string username);
        bool Some();
    }

    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUnitOfWork<IChatChallengeDbContext> _uow;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager, IUnitOfWork<IChatChallengeDbContext> uow)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _uow = uow;
        }

        public async Task<IdentityResult> CreateUser(User user, string password)
        {
            var response = await _userManager.CreateAsync(user, password);

            if (response.Succeeded)
            {
                var claims = new List<Claim>() {
                    new Claim(ClaimTypes.Name, user.UserName)
                };
                return await _userManager.AddClaimsAsync(user, claims);
            }

            return response;
        }

        public async Task<SignInResult> IsValid(string username, string password)
        {
            return await _signInManager.PasswordSignInAsync(username, password, false, false);
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await _uow.context.GetDbSet<User>().Where(x => x.UserName == username).FirstOrDefaultAsync();
        }

        public bool Some()
        {
            return _userManager.Users.Any();
        }
    }
}
