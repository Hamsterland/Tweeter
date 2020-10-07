using System;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Tweeter.Services
{
    public class StartupService : IHostedService
    {
        private readonly DiscordSocketClient _client;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public StartupService(
            DiscordSocketClient client,
            IConfiguration configuration, 
            ILogger logger)
        {
            _client = client;
            _configuration = configuration;
            _logger = logger;
        }
        
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var token = _configuration["Token"];

            if (string.IsNullOrWhiteSpace(token))
            {
                _logger.Fatal("Token not found in appsettings.json");
                return;
            }

            try
            {
                await _client.LoginAsync(TokenType.Bot, token);
                await _client.StartAsync();
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex.Message);
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _client.LogoutAsync();
            await _client.StopAsync();
        }
    }
}