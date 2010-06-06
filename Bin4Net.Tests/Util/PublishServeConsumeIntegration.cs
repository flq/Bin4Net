using System;
using System.IO;
using Bin4Net.Publishing;
using Bin4Net.Tests.CompileScenarios;

namespace Bin4Net.Tests.Util
{
    public class PublishServeConsumeIntegration
    {
        private string assemblyFileName = "testAssembly.dll";
        private readonly string contentPath;

        public PublishServeConsumeIntegration() : this("dwnld")
        {
            
        }

        public PublishServeConsumeIntegration(string contentPath)
        {
            this.contentPath = Path.Combine(Environment.CurrentDirectory, contentPath);
            if (!Directory.Exists(contentPath))
                Directory.CreateDirectory(contentPath);
        }

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

            var c = new TestsCompiler()
              .StoreAssemblyAs(Path.Combine(contentPath, assemblyFileName))
              .With<T>();

            return this;
        }

        public PublishServeConsumeIntegration CreateTorrent(string torrentName, bool useSingleAssembly)
        {
            var p = new Publisher(
                new PublishingOptions
                    {
                        Content = useSingleAssembly ? Path.Combine(contentPath, assemblyFileName) : contentPath,
                        PathToAssembly = Path.Combine(contentPath, assemblyFileName),
                        TorrentName = torrentName,
                        RunInSeparateAppDomain = true
                    });
            return this;
        }
    }
}