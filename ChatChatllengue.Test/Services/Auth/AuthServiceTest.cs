using AutoMapper;
using ChatChallenge.Core.Models;
using ChatChallenge.Model.Contexts.ChatChallenge;
using ChatChallenge.Model.Entities.Security;
using ChatChallenge.Model.UnitOfWorks;
using ChatChallenge.Model.UnitOfWorks.ChatChallenge;
using ChatChallenge.Services.Models;
using ChatChallenge.Services.Services.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NUnit.Framework;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ChatChatllengue.Test.Services.Auth
{
    [TestFixture]
    public class AuthServiceTest
    {
        private  IAuthService _authService;

        [SetUp]
        public void Setup()
        {
            var tokenSetting = new TokenSetting()
            {
                Audience = "CHATCHALLENGE-KEY",
                Issuer = "CHATCHALLENGE",
                Secret = "CHATCHALLENGE-CLIENT"
            };
            string myAppDbContextConnection = "{ \"DefaultConnection\": \"Server=.;Database=ChatChallenge;Trusted_Connection=true;MultipleActiveResultSets=true\"}";

            var services = new ServiceCollection();

            services.AddDbContext<ChatChallengeDbContext>(op => op.UseSqlServer(myAppDbContextConnection), ServiceLifetime.Transient);
            services.AddTransient<IChatChallengeDbContext, ChatChallengeDbContext>();
            services.AddScoped<IUnitOfWork<IChatChallengeDbContext>, ChatChallengeUnitOfWork>();

            #region Auth config

            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ChatChallengeDbContext>().AddDefaultTokenProviders();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenSetting.Secret)),
                    ValidIssuer = tokenSetting.Issuer,
                    ValidAudience = tokenSetting.Audience,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddAuthorization();
            #endregion
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddTransient<IUserService, UserService>();
            services.AddSingleton(tokenSetting);
            services.AddTransient<IAuthService, AuthService>();
           

            var serviceProvider = services.BuildServiceProvider();

            _authService = serviceProvider.GetService<IAuthService>();
        }

        [Test]
        public void ShouldReturnToken()
        {
            var user = new User() { 
                UserName = "Jhon23"
            };

            var token = _authService.GetToken(user);

            var validToken = !string.IsNullOrEmpty(token);

            Assert.True(validToken);
        }

    }
}
