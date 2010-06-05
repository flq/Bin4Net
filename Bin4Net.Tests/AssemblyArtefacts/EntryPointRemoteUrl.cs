using Bin4Net.Publish;

namespace Bin4Net.Tests.AssemblyArtefacts
{
  public class EntryPointRemoteUrl
  {
    public static void Bin4Net(IPublisher publisher)
    {
      publisher
        .SetupMetadata(m=> m
                             .FromAssemblyAttributes()
                             .AssociateWithTags("tool", "acme"))
        .Versioning(v=>v.VersionOfThisAssembly())
        .DownloadUnder("http://realfiction.net/bin4net/single/testAssembly.dll");
    }
  }
}