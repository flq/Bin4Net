using Bin4Net.Publish;

namespace Bin4Net.Tests.AssemblyArtefacts
{
  public class EntryPoint3
  {
    public static void Bin4Net(IPublisher publisher)
    {
      publisher
        .SetupMetadata(m=> m
                             .FromAssemblyAttributes()
                             .AssociateWithTags("tool", "acme"))
        .DownloadUnder("http://localhost:8889/");
    }
  }
}