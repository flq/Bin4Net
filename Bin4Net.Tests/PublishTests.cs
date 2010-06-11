using System;
using System.IO;
using Bin4Net.Publish;
using Bin4Net.Publishing;
using Bin4Net.Tests.CompileScenarios;
using Bin4Net.Tests.Util;
using Moq;
using NUnit.Framework;
using EntryPointNotFoundException = Bin4Net.Publishing.EntryPointNotFoundException;

namespace Bin4Net.Tests
{
    [TestFixture]
    public class PublishTests
    {
        [Test]
        [ExpectedException(typeof(EntryPointNotFoundException))]
        public void WhenEntryPointNotFoundExceptionIsThrown()
        {
            var c = new TestsCompiler().With<AttributesOnly>();
            new Publisher(new PublishingOptions(c.Assembly));
        }

        [Test]
        [ExpectedException(typeof(EntryPointNotFoundException))]
        public void MultipleEntryPointsThrow()
        {
            var c = new TestsCompiler().With(new MultiEntryPoint() + "EntryPoint2.cs");
            new Publisher(new PublishingOptions(c.Assembly));
        }

        [Test]
        public void TheEntryPointIsFound()
        {
            try
            {
                var c = new TestsCompiler().With<AttributeAndEntryPoint>();
                new Publisher(new PublishingOptions(c.Assembly));
            }
            catch (EntryPointNotFoundException x)
            {
                Assert.Fail("Entry point was not found: {0}", x.Message);
            }
            catch (EmptyBinException)
            {
                // Irrelevant for this test
            }
        }

        [Test]
        public void PublisherIsProvidedToPublishedItem()
        {
            var pMock = new Mock<IPublisher>();
            pMock.Setup(p => p.SetupMetadata(It.IsAny<Action<IMetadataInlet>>())).Returns(pMock.Object);
            var c = new TestsCompiler().With<AttributeAndEntryPoint>();
            new Publisher(new PublishingOptions(c.Assembly) { CustomPublisher = pMock.Object });
            pMock.Verify();
        }

        [Test]
        public void PublishingOptionsConvertsFileToAssembly()
        {
            var c = new TestsCompiler()
              .StoreAssemblyAs("PublishingOptionsConvertsFileToAssembly.dll")
              .With<AssemblyWithSingleDownload>();
            var options = new PublishingOptions { PathToAssembly = "PublishingOptionsConvertsFileToAssembly.dll" };
            var ass = options.EntryAssembly;
            ass.ShouldNotBeNull();
        }

        [Test]
        public void TheTorrentsDefaultNameIsTheProductName()
        {
            var c = new TestsCompiler()
              .StoreAssemblyAs("defaultName.dll")
              .With<AssemblyWithSingleDownload>();

            string contentPath = Path.Combine(Environment.CurrentDirectory, "defaultName.dll");
            var p = new Publisher(
                new PublishingOptions
                {
                    Content = contentPath,
                    PathToAssembly = contentPath,
                    RunInSeparateAppDomain = true
                });
            File.Exists("acme stuff.torrent").ShouldBeTrue();
        }
    }
}
