using System;
using System.Threading;
using System.Threading.Tasks;
using Discord.Commands;
using Imgur.API;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;

namespace Essence.Modules.Fun
{
  [Group("meme")]
  public class Meme : ModuleBase<SocketCommandContext>
  {
    [Command()]
    public async Task MemeCommand()
    {
      var msg = await ReplyAsync("This command is still being worked on.");
      
      await Context.Message.DeleteAsync();
      
      Thread.Sleep(5000);
      await msg.DeleteAsync();
      
      /*try
      {
        var client = new ImgurClient("c93703697060d5c", "bc304e18172594158d79b5f79dd20f8e09d600f5");
        var endpoint = new ImageEndpoint(client);
        var image = await endpoint.GetImageAsync("IMAGE_ID");
        Console.WriteLine("Image retrieved. Image Url: " + image.Link);
      }
      catch (ImgurException imgurEx)
      {
        Console.WriteLine("An error occurred getting an image from Imgur.");
        Console.WriteLine(imgurEx.Message);
      }*/
    }
  }
}
