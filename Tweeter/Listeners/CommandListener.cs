using System;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tweeter.Notifications;

namespace Tweeter.Listeners
{
    public class CommandListener : INotificationHandler<MessageReceivedNotification>
    {
        private readonly IConfiguration _configuration;
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commandService;
        private readonly IServiceProvider _serviceProvider;

        public CommandListener(
            IConfiguration configuration,
            DiscordSocketClient client,
            CommandService commandService,
            IServiceProvider serviceProvider)
        {
            _configuration = configuration;
            _client = client;
            _commandService = commandService;
            _serviceProvider = serviceProvider;
        }

        public async Task Handle(MessageReceivedNotification notification, CancellationToken cancellationToken)
        {
            if (!(notification.Message is SocketUserMessage message)
                || !(message.Author is IGuildUser user)
                || user.IsBot)
            {
                return;
            }

            var argPos = 0;
            var prefix = _configuration["Prefix"];

            if (message.HasStringPrefix(prefix, ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                var context = new SocketCommandContext(_client, message);
                using var scope = _serviceProvider.CreateScope();
                await _commandService.ExecuteAsync(context, argPos, scope.ServiceProvider);
            }
        }
    }
}