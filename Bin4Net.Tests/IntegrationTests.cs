using System;
using System.IO;
using System.Net;
using System.Threading;
using Bin4Net.Tests.CompileScenarios;
using Bin4Net.Tests.Util;
using NUnit.Framework;

namespace Bin4Net.Tests
{
  [TestFixture]
  public class IntegrationTests
  {
    private HttpFileServer server;
    private readonly string servePath = Path.Combine(Environment.CurrentDirectory, "dwnld");
    private const string assemblyFileName = "myAssembly.dll";

    [TestFixtureSetUp]
    public void Setup()
    {
      if (!Directory.Exists(servePath))
        Directory.CreateDirectory(servePath);

      var c = new TestsCompiler()
        .StoreAssemblyAs(Path.Combine(servePath, assemblyFileName))
        .With<AssemblyWithDownload>();
      
      server = new HttpFileServer(servePath, 8889);
      
    }

    [TestFixtureTearDown]
    public void TearDown()
    {
      server.Dispose();
      Directory.Delete(servePath, true);
    }

    [Test]
    public void BasicSetupWorks()
    {
      Directory.Exists(servePath).ShouldBeTrue();
      var files = Directory.GetFiles(servePath);
      files.ShouldHaveCount(1);
      files[0].Contains(assemblyFileName).ShouldBeTrue();
      using (var wc = new WebClient())
      {
        var bytes = wc.DownloadData("http://localhost:8889/myAssembly.dll");
        bytes.Length.ShouldBeGreaterThan(0);
      }
    }

    [Test]
    public void ThePublisherCreatesTheTorrentFileWithTheProvidedName()
    {
      var p = new Publisher(
        new PublishingOptions
          {
            Content = servePath,
            PathToAssembly = Path.Combine(servePath, assemblyFileName),
            TorrentName = "sweesh.torrent",
            RunInSeparateAppDomain = true
          });

      File.Exists("sweesh.torrent").ShouldBeTrue();
      File.Delete("sweesh.torrent");
    }

    
    

    [Test]
    public void CreatingAPackage_DownloadIt()
    {
      //using (var s = new HttpFileServer(@"C:\Users\flq\Documents"))
      //{
      //  Thread.Sleep(300000);
      //}
    }
  }
}