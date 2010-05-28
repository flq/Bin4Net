using System;
using Bin4Net.Publish;
using Bin4Net.Publishing.PublishCommands;

namespace Bin4Net.Publishing.Inlets
{
    internal class VersioningInlet : IVersioningInlet
    {
        private readonly PublishInlet publishInlet;

        public VersioningInlet(PublishInlet publishInlet)
        {
            this.publishInlet = publishInlet;
        }

        public IVersioningInlet VersionOfThisAssembly()
        {
            publishInlet.Add(new VersionFromAssemblyCommand());
            return this;
        }
    }
}