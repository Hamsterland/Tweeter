using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using Tweeter.Services;
using Tweeter.Services.Services;

namespace Tweeter
{
    public class Program
    {
        private static async Task Main(string[] args) => await Host.CreateDefaultBuilder(args)
            .UseSerilog((context, configuration) =>
            {
                configuration
                    .Enrich.FromLogContext()
                    .MinimumLevel.Verbose()
                    .WriteTo.Console(theme: SystemConsoleTheme.Literate);
            })
            .ConfigureAppConfiguration((context, builder) =>
            {
                builder
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");
            })
            .ConfigureServices((context, collection) =>
            {
                // var configuration = context.Configuration;
                
                var discordSocketClient = new DiscordSocketClient(new DiscordSocketConfig
                {
                    AlwaysDownloadUsers = true,
                    MessageCacheSize = 10000,
                    LogLevel = LogSeverity.Info
                });
                
                var commandService = new CommandService(new CommandServiceConfig
                {
                    LogLevel = LogSeverity.Debug,
                    DefaultRunMode = RunMode.Sync,
                    CaseSensitiveCommands = false,
                    IgnoreExtraArgs = false
                });

                collection
                    .AddMediatR(typeof(StartupService).Assembly)
                    .AddSingleton(discordSocketClient)
                    .AddSingleton(provider =>
                    {
                        commandService.AddModulesAsync(Assembly.GetEntryAssembly(), provider);
                        return commandService;
                    })
                    //.AddDbContext<TweeterContext>(options => options.UseNpgsql(configuration["Postgres:Connection"]))
                    .AddHostedService<StartupService>()
                    .AddHostedService<DiscordSocketListener>();
            })
            .RunConsoleAsync();
    }
}