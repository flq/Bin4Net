using System.IO;
using Bin4Net.Consuming;
using MonoTorrent.BEncoding;

namespace Bin4Net.Frame
{
    public static class TorrentUtilities
    {
        public static BinTorrent AsBinTorrent(this string filename)
        {
            using (var fs = File.OpenRead(filename))
              return new BinTorrent(BEncodedDictionary.DecodeTorrent(fs));
        }
    }
}