using ChatChallenge.Core.Models;
using Microsoft.Extensions.Configuration;
using NServiceBus;
using NServiceBus.Features;
using NServiceBus.Persistence.Sql;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;

namespace ChatChallenge.Bus
{
    class Program
    {
        private static IConfigurationRoot configuration;

        static async Task Main(string[] args)
        {
            configuration = GetConfigurationRoot();
            var nBusConfig = configuration.GetSection("NServiceBusConfig").Get<NServiceBusAppSettings>();

            var endpointConfig = GetEndPointConfiguration(nBusConfig.PublisherName, nBusConfig.ConnectionString);

            var endpointInstance = await Endpoint.Start(endpointConfig).ConfigureAwait(false);
            Console.CancelKeyPress += (s, e) => endpointInstance.Stop().ConfigureAwait(false).GetAwaiter().GetResult();
            Console.WriteLine("\r\nPress enter key to stop program\r\n");
            Console.Read();
        }

        internal static IConfigurationRoot GetConfigurationRoot()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true);
            return builder.Build();
        }

        static EndpointConfiguration GetEndPointConfiguration(string endpointName, string connectionString)
        {
            var endpointConfiguration = new EndpointConfiguration(endpointName);
            var transport = endpointConfiguration
                .UseTransport<SqlServerTransport>()
                .ConnectionString(connectionString);

            endpointConfiguration.EnableInstallers();
            endpointConfiguration.DisableFeature<TimeoutManager>();

            var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
            var subscriptions = persistence.SubscriptionSettings();
            subscriptions.CacheFor(TimeSpan.FromMinutes(1));
            var dialect = persistence.SqlDialect<SqlDialect.MsSqlServer>();
            dialect.Schema("dbo");
            persistence.ConnectionBuilder(
                connectionBuilder: () => new SqlConnection(connectionString));

            return endpointConfiguration;
        }
    }
}
