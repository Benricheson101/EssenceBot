using System;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace Essence.Modules.Moderation
{
  public class UserInfo : ModuleBase<SocketCommandContext>
  {
    [Command("user"), Alias("userinfo", "about", "who")]
    public async Task UserInformation(IGuildUser user = null)
    {
      if (user == null)
        user = (IGuildUser) Context.User;
        
      await Context.Message.DeleteAsync();

      var joinDate = user.JoinedAt.Value.Date.Month + "/" + user.JoinedAt.Value.Date.Day + "/" + user.JoinedAt.Value.Date.Year;
      var joinTime2 = user.JoinedAt.Value.Hour + ":" + user.JoinedAt.Value.Minute;
      //DateTime joinTime = new DateTime();
      
      //joinTime = joinTime.ToString()

      var ebUserInfo = new EmbedBuilder()
        .WithTitle("Information on: " + user.Username)
        .WithAuthor($"{user.Username}#{user.Discriminator}", Context.User.GetAvatarUrl())

        .AddField("Username:", user.Username, true)
        .AddField("Discriminator:", user.Discriminator, true)
        .AddField("Nickname:", user.Nickname, true)


        .AddField("User ID:", user.Id, true)
        .AddField("Highest Role:", Context.Guild.GetRole(user.RoleIds.LastOrDefault()).Mention, true)
        .AddField("Avatar URL:",
          $"[256px]({user.GetAvatarUrl(ImageFormat.Png, 256)}) [512px]({user.GetAvatarUrl(ImageFormat.Png, 512)}) [1024px]({user.GetAvatarUrl(ImageFormat.Png, 1024)})",
          true)

        .AddField("Join Date:", joinDate + " at " + joinTime2, true)
        .AddField("Deafened:", user.IsDeafened || user.IsSelfDeafened, true)
        .AddField("Muted:", user.IsMuted || user.IsSelfMuted, true)

        .AddField("Status:", user.Status, true)
        .AddField("Status Type:", user.Activity.Type, true)
        .AddField("Playing Status:", user.Activity, true)

        .AddField("Created Account:", user.CreatedAt.Date, true);
              
      var msg = await ReplyAsync("", embed: ebUserInfo.Build());
    }
  }
}
