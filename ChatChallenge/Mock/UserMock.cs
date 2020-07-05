using ChatChallenge.Model.Contexts.ChatChallenge;
using ChatChallenge.Model.Entities.Security;
using ChatChallenge.Services.Services.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatChallenge.Mock
{
    public static class UserMock
    {
        public async static void CreateFakeUsers(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
                if (!userService.Some())
                {
                    await userService.CreateUser(new User{ UserName = "Jhon23", FullName = "Jhon Smith" }, "9j^9`5'ew7D,7g'Z");
                    await userService.CreateUser(new User { UserName = "Leandro12", FullName = "Leandro Jimenez" }, ">,F-hT)/h(2BcGqH");
                }
            }
        }
    }
}
