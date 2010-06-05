using System;

namespace Bin4Net.Consuming
{
  public class RepositoryItem
  {
      private readonly string path;

      public RepositoryItem(string path)
      {
          this.path = path;
      }

      public string PhysicalName { get { return path; } }
  }
}