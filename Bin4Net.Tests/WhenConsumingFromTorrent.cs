using System;
using System.IO;
using System.Threading;
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
            setUpAssembly();
            createTorrent("test.torrent");
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
            t.Publisher.ShouldBeEqualTo("acme inc.");
            t.Product.ShouldBeEqualTo("acme stuff");
        }

    }
}