using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Bin4Net.Consuming;
using Bin4Net.Frame;
using Bin4Net.Tests.Util;
using NUnit.Framework;

namespace Bin4Net.Tests
{
    [TestFixture]
    public class TorrentDownloading
    {
        private const string TestTorrentFile = @"..\..\torrents\test.torrent";

        [Test]
        public void BinTorrentObtainsTheRelevantInformation()
        {
            var t = TestTorrentFile.AsBinTorrent();
            t.ShouldNotBeNull();
            t.Version.ShouldBeEqualTo("1.0.0.0");
            t.Publisher.ShouldBeEqualTo("acme inc.");
            t.Product.ShouldBeEqualTo("acme stuff");
        }

        [Test]
        public void GetTorrentDepositsTorrentAtRoot()
        {
            using (var binRep = new TestingBinRepository())
            {
                var bt = TestTorrentFile.AsBinTorrent();
                binRep.Put(bt);
                var item = binRep.Get("acme inc./acme stuff/1.0.0.0");
                item.BinTorrent.ShouldNotBeNull();
                item.BinTorrent.Product.ShouldBeEqualTo("acme stuff");
            }
        }


        [Test]
        public void KnownTorrentWithRemoteAssemblyIsDownloadable()
        {
            var handle = new ManualResetEvent(false);
            using (var binRep = new TestingBinRepository())
            using (var tEnv = new TorrentEnvironment())
            {
                Console.WriteLine("Binrep Root: {0}", binRep.Root);
                var d = new Downloader(binRep, tEnv);
                var sw = Stopwatch.StartNew();
                d.GetBin(TestTorrentFile, handle);
                handle.WaitOne();
                Console.WriteLine(sw.ElapsedMilliseconds);
                var binContents = binRep.Get("acme inc./acme stuff/1.0.0.0").ToList();
                binContents[0].PhysicalName.ShouldBeEqualTo("testAssembly.dll");
            }
        }
    }
}