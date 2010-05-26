using System.IO;
using System.Net;
using System.Threading;
using Bin4Net.Publishing;
using Bin4Net.Tests.Util;
using NUnit.Framework;

namespace Bin4Net.Tests
{
  [TestFixture]
  public class IntegrationTests : AssemblyServicing
  {
    [TestFixtureSetUp]
    public void Setup()
    {
      setUpWebServerAndAssembly();
    }

    [TestFixtureTearDown]
    public void TearDown()
    {
      removeWebServer();
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
      createTorrent("test.torrent");

      File.Exists("test.torrent").ShouldBeTrue();
      File.Delete("test.torrent");
    }
  }
}