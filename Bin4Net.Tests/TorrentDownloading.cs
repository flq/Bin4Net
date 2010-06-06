using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Bin4Net.Consuming;
using Bin4Net.Frame;
using Bin4Net.Tests.AssemblyArtefacts;
using Bin4Net.Tests.CompileScenarios;
using Bin4Net.Tests.Util;
using NUnit.Framework;

namespace Bin4Net.Tests
{
    [TestFixture]
    public class TorrentDownloading
    {
        private const string SingleTorrentFile = @"..\..\torrents\single.torrent";
        private const string MultiTorrentFile = @"..\..\torrents\multi.torrent";

        [Test,Ignore]
        public void CreateTestTorrents()
        {
            var i = new PublishServeConsumeIntegration("single")
                .SetAssemblyName("testAssembly.dll")
                .CreateAssembly<AssemblyWithSingleDownload>()
                .CreateTorrent("single.torrent", true);

            i = new PublishServeConsumeIntegration("multi")
                .SetAssemblyName("utility.dll")
                .CreateAssembly<UtilityAssembly>();
            i.SetAssemblyName("multiAssembly.dll")
                .CreateAssembly<MultiEntryPoint>();

            i.CreateTorrent("multi.torrent", false);
        }

        [Test]
        public void BinTorrentObtainsTheRelevantInformation()
        {
            var t = SingleTorrentFile.AsBinTorrent();
            t.ShouldNotBeNull();
            t.Version.ShouldBeEqualTo("1.0.0.0");
            t.Publisher.ShouldBeEqualTo("acme inc.");
            t.Product.ShouldBeEqualTo("acme stuff");
        }

        [Test]
        public void BinTorrent_ObtainsTheRelevantInformation_FromMultiFileBin()
        {
            var t = MultiTorrentFile.AsBinTorrent();
            t.ShouldNotBeNull();
            t.Version.ShouldBeEqualTo("1.1.0.0");
            t.Publisher.ShouldBeEqualTo("acme inc.");
            t.Product.ShouldBeEqualTo("acme multi");
            var files = t.ToList();
            files.ShouldHaveCount(2);
        }

        [Test]
        public void GetTorrentDepositsTorrentAtRoot()
        {
            using (var binRep = new TestingBinRepository())
            {
                var bt = SingleTorrentFile.AsBinTorrent();
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
                d.GetBin(SingleTorrentFile, handle);
                handle.WaitOne();
                Console.WriteLine(sw.ElapsedMilliseconds);
                var binContents = binRep.Get("acme inc./acme stuff/1.0.0.0").ToList();
                binContents[0].PhysicalName.ShouldBeEqualTo("testAssembly.dll");
            }
        }

        [Test]
        public void KnownTorrentWithSeveralItemsIsDownloadable()
        {
            var handle = new ManualResetEvent(false);
            using (var binRep = new TestingBinRepository())
            using (var tEnv = new TorrentEnvironment())
            {
                Console.WriteLine("Binrep Root: {0}", binRep.Root);
                var d = new Downloader(binRep, tEnv);
                var sw = Stopwatch.StartNew();
                d.GetBin(MultiTorrentFile, handle);
                handle.WaitOne();
                Console.WriteLine(sw.ElapsedMilliseconds);
                var binContents = binRep.Get("acme inc./acme multi/1.1.0.0").ToList();
                binContents.ShouldHaveCount(2);
                binContents.Any(ri=>ri.PhysicalName == "multiAssembly.dll").ShouldBeTrue();
                binContents.Any(ri => ri.PhysicalName == "utility.dll").ShouldBeTrue();
            }
        }
    }
}