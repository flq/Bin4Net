using System;

namespace Bin4Net.PublishCommands
{
  public class AddWebSeedCommand : PublishCommand
  {
    private readonly string url;

    public AddWebSeedCommand(string url)
    {
      this.url = url;
    }

    protected override void execute()
    {
      context.Seed.AddWebSeed(url);
    }
  }
}