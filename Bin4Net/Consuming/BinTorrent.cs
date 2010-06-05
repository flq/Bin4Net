using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using MonoTorrent.BEncoding;
using MonoTorrent.Common;
using System.Linq;

namespace Bin4Net.Consuming
{
    public class BinTorrent : IEnumerable<string>
    {
        private readonly Torrent torrent;
        private readonly BEncodedDictionary dictionary;

        public BinTorrent(BEncodedDictionary torrent)
        {
            dictionary = torrent;
            this.torrent = Torrent.Load(torrent);
        }

        public string Publisher
        {
            get
            {
                return torrent.Publisher;
            }
        }

        public string Product
        {
            get
            {
                return ((BEncodedString)dictionary[new BEncodedString("product")]).Text;
            }
        }

        public string Version
        {
            get
            {
                return ((BEncodedString) dictionary[new BEncodedString("version")]).Text;
            }
        }

        public string TorrentName
        {
            get { return torrent.Name; }
        }

        public static implicit operator Torrent(BinTorrent t)
        {
            return t.torrent;
        }

        /// <summary>
        /// Enumerates all files contained in this bin as paths
        /// </summary>
        public IEnumerator<string> GetEnumerator()
        {
            return torrent.Files.Select(f => f.Path).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void StoreTo(Stream stream)
        {
            var bytes = torrent.ToBytes();
            stream.Write(bytes, 0, bytes.Length);
        }
    }
}