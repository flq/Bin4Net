using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bin4Net.Frame;
using MonoTorrent.BEncoding;

namespace Bin4Net.Consuming
{
    public class Bin : IEnumerable<RepositoryItem>
    {
        private readonly string path;
        private BinTorrent binTorrent;

        public Bin(string path)
        {
            this.path = path;
        }

        public BinTorrent BinTorrent
        {
            get
            {
                if (binTorrent == null)
                {
                    var file = Directory.GetFiles(path)
                        .FirstOrDefault(f => ".torrent".Equals(Path.GetExtension(f)));
                    binTorrent = file.AsBinTorrent();
                }
                return binTorrent;
            }
        }

        public IEnumerator<RepositoryItem> GetEnumerator()
        {
            return BinTorrent.Select(path => new RepositoryItem(path)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}