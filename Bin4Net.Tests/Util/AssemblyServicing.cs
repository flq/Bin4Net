using System;
using System.IO;
using Bin4Net.Publishing;
using Bin4Net.Tests.CompileScenarios;

namespace Bin4Net.Tests.Util
{
  public class AssemblyServicing
  {
    protected const string assemblyFileName = "myAssembly.dll";
    protected internal HttpFileServer server;
    protected internal readonly string servePath = Path.Combine(Environment.CurrentDirectory, "dwnld");

    protected void setUpWebServerAndAssembly()
    {
      if (!Directory.Exists(servePath))
        Directory.CreateDirectory(servePath);

      var c = new TestsCompiler()
        .StoreAssemblyAs(Path.Combine(servePath, assemblyFileName))
        .With<AssemblyWithDownload>();
      
      server = new HttpFileServer(servePath, 8889);
    }

    protected void removeWebServer()
    {
      server.Dispose();
      Directory.Delete(servePath, true);
    }

    protected void createTorrent(string torrentName)
    {
      var p = new Publisher(
        new PublishingOptions
          {
            Content = servePath,
            PathToAssembly = Path.Combine(servePath, assemblyFileName),
            TorrentName = torrentName,
            RunInSeparateAppDomain = true
          });
    }
  }
}