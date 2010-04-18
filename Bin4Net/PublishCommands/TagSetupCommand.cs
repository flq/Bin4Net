namespace Bin4Net.PublishCommands
{
  internal class TagSetupCommand : PublishCommand
  {
    public string[] Tags { get; private set; }

    public TagSetupCommand(params string[] tags)
    {
      Tags = tags;
    }

    protected override void execute()
    {
      
    }
  }
}