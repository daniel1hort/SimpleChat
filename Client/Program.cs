using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Orleans;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        private static readonly ILogger<ClusterClientHostedService> _logger;

        public static Task Main(string[] args)
        {

            return new HostBuilder()
                .ConfigureServices(services =>
                {
                    services.AddSingleton<IClusterClient>(sp => CreateCluserClient(sp).Result);

                    services.AddHostedService<ChatClientHostedService>();

                    services.Configure<ConsoleLifetimeOptions>(options =>
                    {
                        options.SuppressStatusMessages = true;
                    });
                })
                .ConfigureLogging(builder =>
                {
                    builder.AddConsole();
                })
                .RunConsoleAsync();
        }

        private static Task<IClusterClient> CreateCluserClient(IServiceProvider serviceProvider)
        {
            var logger = serviceProvider.GetService<ILogger>();
            var loggerProvider = serviceProvider.GetService<ILoggerProvider>();

            var client = new ClientBuilder()
                .UseLocalhostClustering()
                .ConfigureLogging(builder => builder.AddProvider(loggerProvider))
                .Build();

            var attempt = 0;
            var maxAttempts = 3;
            var delay = TimeSpan.FromSeconds(3);
            var result = client.Connect(async error =>
            {

                if (++attempt < maxAttempts)
                {
                    _logger.LogWarning(error,
                        "Failed to connect to Orleans cluster on attempt {@Attempt} of {@MaxAttempts}.",
                        attempt, maxAttempts);

                    try
                    {
                        await Task.Delay(delay);
                    }
                    catch (OperationCanceledException)
                    {
                        return false;
                    }

                    return true;
                }
                else
                {
                    _logger.LogError(error,
                        "Failed to connect to Orleans cluster on attempt {@Attempt} of {@MaxAttempts}.",
                        attempt, maxAttempts);

                    return false;
                }
            });

            return Task.FromResult(client);
        }
    }
}
