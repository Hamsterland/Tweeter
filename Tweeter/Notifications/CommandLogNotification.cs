using Discord;
using MediatR;
using Serilog;

namespace Tweeter.Notifications
{
    public class CommandLogNotification : INotification
    {
        public LogMessage Message;

        public CommandLogNotification(LogMessage message)
        {
            Message = message;
        }
    }
}