using System;
using System.Diagnostics;
using Bin4Net.Consuming;
using MonoTorrent.Client;

namespace Bin4Net
{
    public class TorrentEnvironment : IDisposable
    {
        private readonly ClientEngine engine;

        public TorrentEnvironment() : this(new TorrentEnvironmentSetup()) { }

        private TorrentEnvironment(TorrentEnvironmentSetup torrentEnvironmentSetup)
        {
            engine = new ClientEngine(new EngineSettings(null, 50737) {  });
        }

        internal TorrentManager PrepareManager(BinRootInfo bt)
        {
            //TODO: I suppose managers need to go once the torrent is finished, unless
            // the user wants to seed what he downloaded. We will see...
            var m = new TorrentManager(bt.Torrent, bt.FullPath, new TorrentSettings() { });
            engine.Register(m);
            return m;
        }

        public void Dispose()
        {
            engine.Dispose();
        }
    }

    public class TorrentEnvironmentSetup
    {

    }
}