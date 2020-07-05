using ChatChallenge.Model.Contexts.ChatChallenge;
using ChatChallenge.Model.UnitOfWorks;
using ChatChallenge.Model.UnitOfWorks.ChatChallenge;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatChallenge.Model.IoC
{
    public static class ModelRegistry
    {
        public static void AddModelRegistry(this IServiceCollection services)
        {
            services.AddTransient<IChatChallengeDbContext, ChatChallengeDbContext>();
            services.AddScoped<IUnitOfWork<IChatChallengeDbContext>, ChatChallengeUnitOfWork>();
        }
    }
}
