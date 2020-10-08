using System;
using System.Text;
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
        public async Task Tweet([Remainder] string content)
        {
            var sb = new StringBuilder();
            sb.AppendLine(content);
            
            var retweets = _random.Next(Constants.Retweets.Start.Value, Constants.Retweets.End.Value);
            var likes = _random.Next(Constants.Likes.Start.Value, Constants.Likes.End.Value);
            var user = (IGuildUser) Context.User;

            var embed = new EmbedBuilder()
                .SetTweetDefaults(user, retweets, likes)
                .WithAttatchments(sb, Context.Message.Attachments)
                .WithDescription(sb.ToString())
                .Build();

            await ReplyAsync(embed: embed);
        }
    }
}