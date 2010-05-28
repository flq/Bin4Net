using Bin4Net.Consuming;
using Bin4Net.Tests.Util;
using NUnit.Framework;
using System.Linq;

namespace Bin4Net.Tests
{
  [TestFixture]
  public class WhenConumingFromTorrent : AssemblyServicing
  {
    [TestFixtureSetUp]
    public void Given()
    {
      setUpWebServerAndAssembly();
      createTorrent("test.torrent");
    }

    [Test]
    public void TheDownloaderObtainsTheCorrectFiles()
    {
      var binRep = new TestingBinRepository();
      var d = new Downloader(binRep);
      d.GetBin("test.torrent");
      var binContents = binRep.Get("acme inc./acme stuff/1.0.0.0").ToList();
      binContents.ShouldHaveCount(1);
      binContents[0].PhysicalName.ShouldBeEqualTo("myAssembly.dll");
    }
  }
}