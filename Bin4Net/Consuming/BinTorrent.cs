using MonoTorrent.BEncoding;
using MonoTorrent.Common;

namespace Bin4Net.Consuming
{
    public class BinTorrent
    {
        private readonly Torrent torrent;
        private readonly BEncodedDictionary dictionary;

        public BinTorrent(BEncodedDictionary torrent)
        {
            dictionary = torrent;
            this.torrent = Torrent.Load(torrent);
        }

        public string Version
        {
            get
            {
                return ((BEncodedString) dictionary[new BEncodedString("version")]).Text;
            }
        }
    }
}