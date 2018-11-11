using System;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Discord;
using Discord.Commands;

namespace Essence.Modules.Moderation
{
  public class Info : ModuleBase<SocketCommandContext>
  {
    [Command("user"), Alias("userinfo", "about", "aboutuser", "who", "whois", "uinfo", "uabout")]
    public async Task UserInformation(IGuildUser user = null)
    {
      Console.WriteLine($"User {Context.User.Username} ran the command: {Context.Message.Content}");
      
      if (user == null)
        user = (IGuildUser) Context.User;
        
      var joinDate = user.JoinedAt.Value.Date.Month + "/" + user.JoinedAt.Value.Date.Day + "/" + user.JoinedAt.Value.Date.Year;
      var joinTime2 = user.JoinedAt.Value.Hour + ":" + user.JoinedAt.Value.Minute;
      var nickname = user.Nickname;
      if (user.Nickname == null)
        nickname = "Null";
      var serverOwner = user.Guild.GetOwnerAsync().Result.Id;
      bool isServerOwner = serverOwner == user.Id;
      var currentVoiceChannel = user.VoiceChannel;
      string inVC;
      if (user.VoiceChannel == null)
        inVC = "Null";
      else
        inVC = user.VoiceChannel.Name;


      Console.WriteLine("Creating Embed");

      var ebUserInfo = new EmbedBuilder()
        .WithTitle("Information on: " + user.Username)
        .WithAuthor($"{user.Username}#{user.Discriminator}", Context.User.GetAvatarUrl())

        .AddField("Username:", user.Username, true)
        .AddField("Discriminator:", user.Discriminator, true)
        .AddField("Nickname:", nickname, true)

        .AddField("User ID:", user.Id, true)
        .AddField("Avatar URL:",
          $"[256px]({user.GetAvatarUrl(ImageFormat.Png, 256)}) [512px]({user.GetAvatarUrl(ImageFormat.Png, 512)}) [1024px]({user.GetAvatarUrl(ImageFormat.Png, 1024)})",
          true)
        .AddField("Join Date:", joinDate + " at " + joinTime2, true)

        .AddField("Server Owner:", isServerOwner, true)        
        .AddField("Highest Role:", Context.Guild.GetRole(user.RoleIds.LastOrDefault()).Mention, true)
        .AddField("Created Account:", user.CreatedAt.Date.Month + "/" + user.CreatedAt.Day + "/" + user.CreatedAt.Year, true)
          
        .AddField("In Voice Channel:", inVC, true)
        .AddField("Deafened:", user.IsDeafened || user.IsSelfDeafened, true)
        .AddField("Muted:", user.IsMuted || user.IsSelfMuted, true)
          
        .AddField("Status:", user.Status, true)
        .AddField("Status Type:", user.Activity.Type, true)
        .AddField("Playing Status:", user.Activity, true);


      Console.WriteLine("Sending Embed");
      
      var msg = await ReplyAsync("", embed: ebUserInfo.Build());
    }
  }
}
