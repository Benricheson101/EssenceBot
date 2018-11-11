using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Essence.Resources.DataTypes;
using Essence.Resources.Settings;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Essence
{
  class Program
  {
    private static void Main(string[] args)
      => new Program().MainAsync().GetAwaiter().GetResult();

    private DiscordSocketClient _client;
    private CommandService _command;
    private IServiceProvider _services;



    private async Task MainAsync()
    {
      
      _client = new DiscordSocketClient(new DiscordSocketConfig
      {
        LogLevel = LogSeverity.Debug
      });
      
      _command = new CommandService(new CommandServiceConfig
      {
        CaseSensitiveCommands = false,
        DefaultRunMode = RunMode.Async,
        LogLevel = LogSeverity.Debug
      });

      _services = new ServiceCollection()
        .AddSingleton(_client)
        .AddSingleton(_command)
        .BuildServiceProvider();

      _client.Log += Client_Log;
      _client.Ready += Client_Ready;

      //——————————[ JSON ]——————————\\

      var json = "";
      var settingsLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)
        .Replace(@"bin/Debug/netcoreapp2.1", @"Resources/Settings/settings.json");
      using (var stream = new FileStream(settingsLocation, FileMode.Open, FileAccess.Read))
      using (var readSettings = new StreamReader(stream))
      {
        json = readSettings.ReadToEnd();
      }

      var settings = JsonConvert.DeserializeObject<Settings>(json);

      BotSettings.Token = settings.Token;
      BotSettings.Playing = settings.Playing;
      BotSettings.Prefix = settings.Prefix;
      BotSettings.DeleteDelay = settings.DeleteDelay;
      
      //––––––––––[ Register Commands ]––––––––––\\

      await RegisterCommandsAsync();
      await _client.LoginAsync(TokenType.Bot, BotSettings.Token);
      await _client.StartAsync();
      await Task.Delay(-1);

    }



    //––––––––––[ Setup ]––––––––––\\

    private async Task Client_Log(LogMessage arg)
    {
      Console.WriteLine($"[{DateTime.Now} at {arg.Source}] {arg.Message}");
      return;
    }
    
    private async Task Client_Ready()
    {
      await _client.SetGameAsync($"{BotSettings.Prefix} | {BotSettings.Playing}", "", ActivityType.Watching);
    }
    
    private async Task RegisterCommandsAsync()
    {
      _client.MessageReceived += HandleCommandAsync;
      await _command.AddModulesAsync(Assembly.GetEntryAssembly());
    }

    //––––––––––[ Command Handler ]––––––––––\\
    
    private async Task HandleCommandAsync(SocketMessage arg)
    {
      var message = arg as SocketUserMessage;

      if (message == null || message.Author.IsBot)
        return;
      
      var argPos = 0;

      if (message.HasStringPrefix(BotSettings.Prefix, ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos))
      {
        var context = new SocketCommandContext(_client, message);
        var result = await _command.ExecuteAsync(context, argPos, _services);
        
        if (!result.IsSuccess)
          Console.WriteLine($"[{DateTime.Now} at {arg.Source}] {result.ErrorReason}");
      }
    }
    
    //––––––––––[ Giphy ]––––––––––\\
    

  }
}
