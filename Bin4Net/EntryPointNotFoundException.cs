using System;

namespace Bin4Net
{
  public class EntryPointNotFoundException : Exception
  {
    public EntryPointNotFoundException()
    {
    }

    public EntryPointNotFoundException(string message) : base(message)
    {
    }

    public EntryPointNotFoundException(string message, Exception inner) : base(message, inner)
    {
    }
  }
}