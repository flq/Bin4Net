using System;
using Bin4Net.Publish;
using Bin4Net.Publishing.PublishCommands;

namespace Bin4Net.Publishing.Inlets
{
    internal class DownloadInlet : IDownloadInlet
    {
        private readonly PublishInlet publishInlet;

        public DownloadInlet(PublishInlet publishInlet)
        {
            this.publishInlet = publishInlet;
        }

        public IDownloadInlet FixedLocation(string url)
        {
            publishInlet.Add(new AddWebSeedCommand(url));
            return this;
        }
    }
}