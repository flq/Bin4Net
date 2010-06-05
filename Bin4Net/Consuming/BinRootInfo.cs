using System.IO;

namespace Bin4Net.Consuming
{

    internal class BinRootInfo
    {
        private readonly string basePath;
        private readonly BinTorrent torrent;
        private readonly string relativePath;

        public BinRootInfo(string basePath, BinTorrent torrent)
        {
            this.basePath = basePath;
            this.torrent = torrent;
            relativePath = torrent.Publisher + "\\" + torrent.Product + "\\" + torrent.Version;
        }

        public BinTorrent Torrent
        {
            get { return torrent; }
        }

        public string RelativePath
        {
            get { return relativePath; }
        }

        public string BasePath
        {
            get { return basePath; }
        }

        public string FullPath
        {
            get
            {
                return Path.Combine(basePath, relativePath);
            }
        }
    }
}