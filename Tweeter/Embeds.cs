using System.Collections.Generic;
using System.Linq;
using System.Text;
using Discord;

namespace Tweeter
{
    public static class Embeds
    {
        public static EmbedBuilder SetTweetDefaults(
            this EmbedBuilder builder, 
            IGuildUser user,
            int retweets,
            int likes)
        {
            return builder
                .WithColor(Constants.EmbedColour)
                .WithUserAsAuthor(user)
                .WithRetweets(retweets)
                .WithTweetFooter()
                .WithLikes(likes);
        }

        public static EmbedBuilder WithRetweets(this EmbedBuilder builder, int retweets)
        {
            return builder.AddField("Retweets", retweets, true);
        }

        public static EmbedBuilder WithLikes(this EmbedBuilder builder, int likes)
        {
            return builder.AddField("Likes", likes, true);
        }

        public static EmbedBuilder WithTweetFooter(this EmbedBuilder builder)
        {
            return builder
                .WithFooter(footer => footer
                        .WithIconUrl("https://i.imgur.com/bgbSd08.png")
                        .WithText("Twitter"));
        }
        
        public static EmbedBuilder WithUserAsAuthor(this EmbedBuilder builder, IGuildUser user)
        {
            var username = user.Nickname ?? user.Username;
            
            return builder
                .WithAuthor(author => author
                        .WithName($"{username} (@{user})")
                        .WithIconUrl(user.GetAvatarUrl()));
        }

        public static EmbedBuilder WithAttatchments(this EmbedBuilder builder, StringBuilder sb, IReadOnlyCollection<Attachment> attatchments)
        {
            if (attatchments.Count == 0)
            {
                return builder;
            }

            builder.WithImageUrl(attatchments.First().Url);

            if (attatchments.Count > 1)
            {
                sb.AppendLine();
                sb.AppendLine(Format.Bold("Attachments"));
                
                foreach (var attatchment in attatchments.Skip(1))
                {
                    sb.AppendLine($"[{attatchment.Filename}]({attatchment.Url})");
                }
            }

            return builder;
        }
    }
}