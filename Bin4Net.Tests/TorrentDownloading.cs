using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Bin4Net.Consuming;
using Bin4Net.Tests.Util;
using NUnit.Framework;

namespace Bin4Net.Tests
{
    [TestFixture]
    public class TorrentDownloading
    {
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
                d.GetBin(@"..\..\torrents\test.torrent", handle);
                handle.WaitOne();
                Console.WriteLine(sw.ElapsedMilliseconds);
                var binContents = binRep.Get("acme inc./acme stuff/1.0.0.0").ToList();
                binContents[0].PhysicalName.ShouldBeEqualTo("testAssembly.dll");
            }
        }
    }
}