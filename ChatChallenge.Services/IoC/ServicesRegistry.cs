using ChatChallenge.Services.Services.Chat;
using ChatChallenge.Services.Services.Security;
using ChatChallenge.Services.Services.Stock;
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
            services.AddTransient<IChatRoomMessageService, ChatRoomMessageService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IStockService, StockService>();
        }
    }
}
