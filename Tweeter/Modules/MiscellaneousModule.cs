using System.Threading.Tasks;
using Discord.Commands;

namespace Tweeter.Services.Modules
{
    [Name("Miscellaneous")]
    [Summary("Miscellaneous commands.")]
    public class MiscellaneousModule : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        [Summary("Pong!")]
        public async Task Ping()
        {
            await ReplyAsync(":ping_pong: Pong!");
        }
    }
}