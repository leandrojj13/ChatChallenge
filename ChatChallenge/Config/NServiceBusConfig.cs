
using ChatChallenge.Core.IoC.Config;
using ChatChallenge.Core.Models;
using ChatChallenge.Hubs;
using ChatChallenge.Messages.Commands;
using ChatChallenge.Messages.Events;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus;
using NServiceBus.Features;
using NServiceBus.Persistence.Sql;
using System;
using System.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ChatChallenge.Config
{
    public static class NServiceBusConfig
    {
        public static void AddNServiceBus(this IServiceCollection services, IConfiguration configuration)
        {
            var nBusConfig = configuration.GetSection("NServiceBusConfig").Get<NServiceBusAppSettings>();
            var endpointConfiguration = new EndpointConfiguration(nBusConfig.SenderName);
            var transport = endpointConfiguration.UseTransport<SqlServerTransport>()
                                                    .ConnectionString(nBusConfig.ConnectionString);

            var routing = transport.Routing();

            routing.RouteToEndpoint(typeof(RequestStockInfo), nBusConfig.PublisherName);
            routing.RegisterPublisher(typeof(ResponseStockInfo), nBusConfig.PublisherName);

            endpointConfiguration.EnableInstallers();
            endpointConfiguration.DisableFeature<TimeoutManager>();

            var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
            var subscriptions = persistence.SubscriptionSettings();
            subscriptions.CacheFor(TimeSpan.FromMinutes(1));
            var dialect = persistence.SqlDialect<SqlDialect.MsSqlServer>();
            dialect.Schema("dbo");
            persistence.ConnectionBuilder(
                connectionBuilder: () => new SqlConnection(nBusConfig.ConnectionString));

            services.AddSingleton<IMessageSession>(x =>
            {
                return Endpoint.Start(endpointConfiguration)
                .GetAwaiter()
                .GetResult();
            });
        }
    }
}
