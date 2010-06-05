using System;
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
           
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            
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