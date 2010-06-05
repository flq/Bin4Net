using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using MonoTorrent.BEncoding;
using MonoTorrent.Client;
using MonoTorrent.Common;

namespace Bin4Net.Consuming
{
    internal class Downloader
    {
        private readonly IBinRepository binRepository;
        private readonly TorrentEnvironment torrentEnvironment;
        private ManualResetEvent currentHandle;


        public Downloader(IBinRepository binRepository, TorrentEnvironment torrentEnvironment)
        {
            this.binRepository = binRepository;
            this.torrentEnvironment = torrentEnvironment;
        }

        public void GetBin(string torrentFile) { GetBin(torrentFile,null); }

        /// <summary>
        /// Gets the bin by providing a path to an available torrent file
        /// </summary>
        public void GetBin(string torrentFile, ManualResetEvent handle)
        {
            if (!File.Exists(torrentFile))
                throw new ArgumentException("torrent " + torrentFile + " could not be found for loading.");

            if (handle != null)
                currentHandle = handle;

            BinTorrent bt;
            using (var s = File.OpenRead(torrentFile))
            {
                bt = new BinTorrent(BEncodedDictionary.DecodeTorrent(s));
            }
            download(bt);
        }

        /// <summary>
        /// Get the bin by specifying the relevant torrent. Use this overload
        /// to get the Torrent from a URL
        /// </summary>
        public void GetBin(Uri torrentFile)
        {

        }

        private void download(BinTorrent bt)
        {
            var info = new BinRootInfo(binRepository.Root, bt);
            var m = torrentEnvironment.PrepareManager(info);
            m.TorrentStateChanged += onTorrentStateChanged;
            m.Start();
        }

        private void onTorrentStateChanged(object sender, TorrentStateChangedEventArgs e)
        {
            if (e.OldState == TorrentState.Downloading && e.NewState == TorrentState.Seeding)
                if (currentHandle != null)
                    currentHandle.Set();

            Debug.WriteLine(e.OldState + ", " + e.NewState);
        }
    }
}