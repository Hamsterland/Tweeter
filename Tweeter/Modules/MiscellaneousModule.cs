using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Microsoft.Extensions.Configuration;

namespace Tweeter.Services.Modules
{
    [Name("Miscellaneous")]
    [Summary("Miscellaneous commands.")]
    public class MiscellaneousModule : ModuleBase<SocketCommandContext>
    {
        private readonly CommandService _commandService;
        private readonly IConfiguration _configuration;

        public MiscellaneousModule(CommandService commandService, IConfiguration configuration)
        {
            _commandService = commandService;
            _configuration = configuration;
        }

        [Command("help")]
        [Alias("h")]
        [Summary("Shows bot help.")]
        public async Task Help()
        {
            var builder = new StringBuilder();
            var prefix = _configuration["Prefix"];
            
            builder.AppendLine($"You can use a command through {Context.Client.CurrentUser.Mention} or the default prefix `{prefix}`");
            builder.AppendFormat("```cs\n");
            
            var commands = _commandService.Commands;
            foreach (var command in commands)
            {
                builder.AppendFormat("{0,-5} # {1,-25}\n", command.Name, command.Summary);
            }
            
            builder.AppendFormat("\n```");
            await ReplyAsync(builder.ToString());
        }

        [Command("about")]
        [Summary("A little bit about myself.")]
        public async Task About()
        {
            var builder = new StringBuilder();

            builder
                .AppendLine($"Hello, I'm {Context.Client.CurrentUser.Mention} - A Discord bot that mocks Tweets.")
                .AppendLine()
                .AppendLine("Please check out my GitHub repository if the magic code that makes me run interests you :)")
                .AppendLine("https://github.com/Hamsterland/Tweeter");

            await ReplyAsync(builder.ToString());
        }
        
        [Command("ping")]
        [Summary("Pong!")]
        public async Task Ping()
        {
            await ReplyAsync(":ping_pong: Pong!");
        }
    }
}