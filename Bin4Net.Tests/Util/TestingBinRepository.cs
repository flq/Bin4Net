using System;
using System.Collections.Generic;
using Bin4Net.Consuming;

namespace Bin4Net.Tests.Util
{
  public class TestingBinRepository : IBinRepository
  {
    public IEnumerable<RepositoryItem> Get(string acmeInc)
    {
      return null;
    }
  }
}