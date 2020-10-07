using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using MediatR;
using Microsoft.Extensions.Hosting;
using Tweeter.Notifications;

namespace Tweeter.Services.Services
{
    public class DiscordSocketListener : IHostedService
    {
        private readonly DiscordSocketClient _discordSocketClient;
        private readonly CommandService _commandService;
        private readonly IMediator _mediator;

        public DiscordSocketListener(
            DiscordSocketClient discordSocketClient,
            IMediator mediator,
            CommandService commandService)
        {
            _discordSocketClient = discordSocketClient;
            _mediator = mediator;
            _commandService = commandService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _discordSocketClient.MessageReceived += MessageReceived;
            _commandService.Log += CommandLog;
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _discordSocketClient.MessageReceived -= MessageReceived;
            _commandService.Log -= CommandLog;
            return Task.CompletedTask;
        }
        
        private async Task MessageReceived(SocketMessage message)
        {
            await _mediator.Publish(new MessageReceivedNotification(message));
        }
        
        private async Task CommandLog(LogMessage message)
        {
            await _mediator.Publish(new CommandLogNotification(message));
        }
    }
}