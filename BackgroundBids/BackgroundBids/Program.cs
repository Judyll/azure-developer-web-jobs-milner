using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BackgroundBids
{
    class Program
    {
        private static IConfiguration Configuration { get; set; }

        static void Main(string[] args)
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables()
                .Build();

            /**
             * Build a host
             */
            var builder = new HostBuilder();
            builder.ConfigureWebJobs(config =>
            {
                config.AddAzureStorageCoreServices(); // Let us deal with diagnotics, etc.
                config.AddServiceBus(options =>
                {
                    options.ConnectionString = Configuration.GetConnectionString("ServiceBusQueue");
                });
            });

            builder.ConfigureLogging(config =>
            {
                config.AddConsole();
            });

            var host = builder.Build();

            using (host)
            {
                host.Run();
            }
        }
    }
}
