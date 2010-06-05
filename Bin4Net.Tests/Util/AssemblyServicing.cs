using System;
using System.IO;
using Bin4Net.Publishing;
using Bin4Net.Tests.CompileScenarios;

namespace Bin4Net.Tests.Util
{
    public class AssemblyServicing
    {
        protected const string assemblyFileName = "testAssembly.dll";
        protected internal readonly string servePath = Path.Combine(Environment.CurrentDirectory, "dwnld");


        protected void setUpAssembly()
        {
            if (!Directory.Exists(servePath))
                Directory.CreateDirectory(servePath);

            var c = new TestsCompiler()
              .StoreAssemblyAs(Path.Combine(servePath, assemblyFileName))
              .With<AssemblyWithSingleDownload>();
        }

        protected void createTorrent(string torrentName)
        {
            var p = new Publisher(
              new PublishingOptions
                {
                    Content = Path.Combine(servePath, assemblyFileName),
                    PathToAssembly = Path.Combine(servePath, assemblyFileName),
                    TorrentName = torrentName,                
                    RunInSeparateAppDomain = true
                });

        }
    }
}