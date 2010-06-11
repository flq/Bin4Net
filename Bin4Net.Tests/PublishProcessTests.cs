using Bin4Net.Publishing;
using Bin4Net.Publishing.Inlets;
using Bin4Net.Publishing.PublishCommands;
using Bin4Net.Tests.CompileScenarios;
using NUnit.Framework;
using Bin4Net.Tests.Util;

namespace Bin4Net.Tests
{
    [TestFixture]
    public class PublishProcessTests
    {
        [Test]
        public void UsingMetadataInletFormulatesTheCorrectCommands()
        {
            var pI = new PublishInlet();
            pI.SetupMetadata(i => i.FromAssemblyAttributes());
            var pC = pI as IPublishCommands;
            pC.Commands.ShouldHaveCount(1);
            pC.Commands[0].ShouldBeOfType<DataFromAssemblyCommand>();
        }

        [Test]
        public void BasicVersioningCreatesVersionCommand()
        {
            var pI = new PublishInlet();
            pI.Versioning(v => v.VersionOfThisAssembly());
            var pC = pI as IPublishCommands;
            pC.Commands.ShouldHaveCount(1);
            pC.Commands[0].ShouldBeOfType<VersionFromAssemblyCommand>();
        }

        [Test]
        public void DataFromAssemblyCommandCorrectlyProvidesInformation()
        {
            var cT = new CommandTester()
              .UsingAssemblyFrom<AttributesOnly>()
              .Test<DataFromAssemblyCommand>();
            cT.Seed.TorrentName.ShouldBeEqualTo("acme stuff.torrent");
            cT.Seed.Publisher.ShouldBeEqualTo("acme inc.");
        }

        [Test]
        public void WebSeedsAreIntroducedCorrectly()
        {
            var cT = new CommandTester()
              .UsingAssemblyFrom<AssemblyWithSingleDownload>()
              .Test(new AddWebSeedCommand("http://localhost:80"));
            cT.Seed.WebSeeds.ShouldHaveCount(1);
        }

        [Test]
        public void VersioningIsAddedCorrectly()
        {
            var cT = new CommandTester()
              .UsingAssemblyFrom<AssemblyWithSingleDownload>()
              .Test(new VersionFromAssemblyCommand());
            cT.Seed.Version.ShouldBeEqualTo("1.0.0.0");
        }

        [Test]
        public void AppendPrependCommandsWorks()
        {
            var inlet = new PublishInlet() as IPublishCommands;
            inlet.Append(new TagSetupCommand(""));
            inlet.Prepend(new DataFromAssemblyCommand());
            var cmds = inlet.Commands;
            cmds.ShouldHaveCount(2);
            cmds[0].ShouldBeOfType<DataFromAssemblyCommand>();
            cmds[1].ShouldBeOfType<TagSetupCommand>();
        }


    }
}