using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Essence.Resources.Settings;

namespace Essence.Modules.Moderation
{
  [Group("report")]
  public class Report : ModuleBase<SocketCommandContext>
  {
    [Command("")]
    public async Task ReportNoArg()
    {
      var eb = new EmbedBuilder();
      
      eb.WithTitle($"Incorrect usage")
        .WithAuthor(Context.User.Username, Context.User.GetAvatarUrl())
        .WithDescription($"Correct usage: \n`{BotSettings.Prefix}report <user/bug> [user tag] <details>`")
        .WithTimestamp(DateTimeOffset.Now)
        .WithColor(Color.Orange);
      
      var msg = await ReplyAsync("", embed: eb.Build());
      await Context.Message.DeleteAsync();
      
      Thread.Sleep(10000);
      await msg.DeleteAsync();
    }

    [Command("bug")]
    public async Task ReportBug([Remainder] string proof)
    {
      var eb = new EmbedBuilder();
      eb.WithAuthor(Context.User.Username, Context.User.GetAvatarUrl())
        .WithTitle($"{Context.User.Username} filed a bug report!")
        .AddField("Report:", proof)
        .WithFooter($"Sent from #{Context.Channel.Name} in {Context.Guild.Name}")
        .WithTimestamp(DateTimeOffset.Now)
        .WithColor(Color.DarkRed);

      var msg = await ReplyAsync(":airplane: Your report has been sent off to the staff team, and should be addressed soon.", embed: eb.Build());
      
      await Context.Client.GetGuild(286942883993354240).GetTextChannel(510646423658954753).SendMessageAsync("", embed: eb.Build());
      await Context.Message.DeleteAsync();
      
      Thread.Sleep(10000);

      await msg.DeleteAsync();
    }
    
    [Command("user")]
    public async Task ReportUser(IUser user, [Remainder] string proof)
    {
      
      var eb = new EmbedBuilder();
      eb.WithAuthor(Context.User.Username, Context.User.GetAvatarUrl())
        .WithTitle($"{Context.User.Username} filed a bug report!")
        .AddField("Report:", proof)
        .WithFooter($"Sent from #{Context.Channel.Name} in {Context.Guild.Name}")
        .WithTimestamp(DateTimeOffset.Now)
        .WithColor(Color.DarkRed);

      var msg = await ReplyAsync(":airplane: Your report has been sent off to the staff team, and should be addressed soon.", embed: eb.Build());
      
      await Context.Client.GetGuild(286942883993354240).GetTextChannel(510646423658954753).SendMessageAsync("", embed: eb.Build());
      await Context.Message.DeleteAsync();
      
      Thread.Sleep(10000);

      await msg.DeleteAsync();
      
      EmbedBuilder ebb = new EmbedBuilder();
    }
  }
}
