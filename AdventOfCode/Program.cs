using System.Threading.Tasks;
using AdventOfCode.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AdventOfCode
{
    class Program
    {
        static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            var task = host.RunAsync();
            var startupInstance = host.Services.GetRequiredService<Startup>();
            startupInstance.Execute();
            return task;
        }

        static IHostBuilder CreateHostBuilder(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                services.AddSingleton<Startup>();
                services.AddSingleton<IInputManager, InputManager>();
            })
            .ConfigureLogging((_, logging) =>
            {
                logging.ClearProviders();
                logging.AddSimpleConsole(options => options.IncludeScopes = true);
                logging.AddDebug();
                //logging.AddEventLog();
            });

            return host;
        }
    }
}
