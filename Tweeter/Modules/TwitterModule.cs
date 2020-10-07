using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace Tweeter.Services.Modules
{
    [Name("Twitter")]
    [Summary("For building fake Twitter messages.")]
    public class TwitterModule : ModuleBase<SocketCommandContext>
    {
        private readonly Random _random = new Random();        
        
        [Command("tweet")]
        [Alias("t")]
        [Summary("Sends a tweet.")]
        public async Task Tweet([Remainder] string message)
        {
            var retweets = _random.Next(Constants.Retweets.Start.Value, Constants.Retweets.End.Value);
            var likes = _random.Next(Constants.Likes.Start.Value, Constants.Likes.End.Value);

            var user = (IGuildUser) Context.User;
            
            var embed = new EmbedBuilder()
                .SetTweetDefaults(user, retweets, likes)
                .WithDescription(message)
                .Build();

            await ReplyAsync(embed: embed);
        }
    }
}