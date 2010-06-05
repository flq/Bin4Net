using System;
using System.IO;
using Bin4Net.Publishing;
using Bin4Net.Tests.CompileScenarios;

namespace Bin4Net.Tests.Util
{
    public class PublishServeConsumeIntegration
    {
        private string assemblyFileName = "testAssembly.dll";
        private readonly string servePath = Path.Combine(Environment.CurrentDirectory, "dwnld");
        
        public PublishServeConsumeIntegration SetAssemblyName(string assemblyName)
        {
            assemblyFileName = assemblyName;
            return this;
        }

        public PublishServeConsumeIntegration CreateAssembly()
        {
            return CreateAssembly<AssemblyWithSingleDownload>();
        }

        public PublishServeConsumeIntegration CreateAssembly<T>() where T : CompileScenario, new()
        {
            if (!Directory.Exists(servePath))
                Directory.CreateDirectory(servePath);

            var c = new TestsCompiler()
              .StoreAssemblyAs(Path.Combine(servePath, assemblyFileName))
              .With<T>();

            return this;
        }

        public PublishServeConsumeIntegration CreateTorrent(string torrentName)
        {
            var p = new Publisher(
                new PublishingOptions
                    {
                        Content = Path.Combine(servePath, assemblyFileName),
                        PathToAssembly = Path.Combine(servePath, assemblyFileName),
                        TorrentName = torrentName,
                        RunInSeparateAppDomain = true
                    });
            return this;
        }
    }
}