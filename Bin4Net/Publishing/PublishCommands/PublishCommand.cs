using System;

namespace Bin4Net.Publishing.PublishCommands
{
  public abstract class PublishCommand
  {
    protected ExecutionContext context;

    public void Execute(ExecutionContext context)
    {
      this.context = context;
      try
      {
        execute();
      }
      catch (Exception x)
      {
        throw new CommandExecutionException(string.Format("Failure from command {0}", this.GetType().Name), x);
      }
    }

    protected abstract void execute();
    
  }
}