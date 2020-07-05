using ChatChallenge.Services.Services.Chat;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatChallenge.Services.IoC
{
    public static class ServicesRegistry
    {
        public static void AddServicesRegistry(this IServiceCollection services)
        {
            services.AddTransient<IChatRoomService, ChatRoomService>();
        }
    }
}
