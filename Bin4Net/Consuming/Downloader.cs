using System;
using System.IO;
using MonoTorrent.Client;
using MonoTorrent.Client.PieceWriters;
using MonoTorrent.Common;

namespace Bin4Net.Consuming
{
    internal class Downloader
    {
        private readonly IBinRepository binRepository;
        private readonly ClientEngine engine;

        public Downloader(IBinRepository binRepository)
        {
            this.binRepository = binRepository;
            engine = new ClientEngine(new EngineSettings(binRepository.Root, 50737));
        }

        /// <summary>
        /// Gets the bin by providing a path to an available torrent file
        /// </summary>
        public void GetBin(string torrentFile)
        {
            if (!File.Exists(torrentFile))
                throw new ArgumentException("torrent " + torrentFile + " could not be found for loading.");

            TorrentManager mgr = new TorrentManager(Torrent.lo);
        }

        /// <summary>
        /// Get the bin by specifying the relevant torrent. Use this overload
        /// to get the Torrent from a URL
        /// </summary>
        public void GetBin(Uri torrentFile)
        {

        }
    }
}