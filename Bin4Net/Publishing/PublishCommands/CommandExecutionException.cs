using System;

namespace Bin4Net.Publishing.PublishCommands
{
  public class CommandExecutionException : Exception
  {
    public CommandExecutionException()
    {
    }

    public CommandExecutionException(string message) : base(message)
    {
    }

    public CommandExecutionException(string message, Exception inner) : base(message, inner)
    {
    }
  }
}