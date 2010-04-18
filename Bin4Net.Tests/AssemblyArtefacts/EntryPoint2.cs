using Bin4Net.Publish;

namespace Bin4Net.Tests.AssemblyArtefacts
{
  public class EntryPoint2
  {
    public static void Bin4Net(IPublisher publisher)
    {
      publisher
        .SetupMetadata(m => m
                              .FromAssemblyAttributes()
                              .AssociateWithTags("tool", "acme"));
    }
  }
}