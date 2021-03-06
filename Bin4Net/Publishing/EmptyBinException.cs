using System;

namespace Bin4Net.Publishing
{
  public class EmptyBinException : Exception
  {

    public EmptyBinException() { }

    public EmptyBinException(string message) : base(message) { }

    public EmptyBinException(string message, Exception inner) : base(message, inner) { }
  }
}