using System.Reflection;

namespace Bin4Net
{
  public class ExecutionContext
  {
    private readonly Assembly entryAssembly;
    private readonly PublishedSeed seed;

    public ExecutionContext(Assembly entryAssembly, PublishedSeed seed)
    {
      this.entryAssembly = entryAssembly;
      this.seed = seed;      
    }

    public Assembly EntryAssembly
    {
      get { return entryAssembly; }
    }

    public PublishedSeed Seed
    {
      get { return seed; }
    }
  }
}