using System.IO;
using Bin4Net.Consuming;
using Bin4Net.Tests.Util;
using MonoTorrent.BEncoding;
using NUnit.Framework;
using System.Linq;

namespace Bin4Net.Tests
{
    [TestFixture]
    public class WhenConsumingFromTorrent : AssemblyServicing
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

        [Test]
        public void BinTorrentObtainsTheRelevantInformation()
        {
            BinTorrent t = null;
            using (var s = File.Open("test.torrent", FileMode.Open))
            {
                t = new BinTorrent(BEncodedDictionary.DecodeTorrent(s));
            }
            t.ShouldNotBeNull();
            t.Version.ShouldBeEqualTo("1.0.0.0");
        }

    }
}