using System;
using System.IO;
using MonoTorrent.BEncoding;
using MonoTorrent.Common;

namespace Bin4Net.Consuming
{
    public class FileBinRepository : IBinRepository
    {
        private readonly string root;

        public FileBinRepository(string root)
        {
            this.root = root;
            Directory.CreateDirectory(root);
        }

        public Bin Get(string binPath)
        {
            string path = rootedPath(binPath);
            return Directory.Exists(path) ? new Bin(path) : null;
        }

        public string Root
        {
            get { return root; }
        }

        public void Put(BinTorrent bt)
        {
            var targetPath = Path.Combine(Path.Combine(bt.Publisher, bt.Product), bt.Version);
            ensureDirectory(targetPath);

            using (var fs = File.OpenWrite(rootedPath(targetPath, "this.torrent")))
                bt.StoreTo(fs);
        }

        private void ensureDirectory(string targetPath)
        {
            var path = rootedPath(targetPath);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        private string rootedPath(string binPath)
        {
            return Path.Combine(root, binPath);
        }

        private string rootedPath(string binPath, string file)
        {
            return Path.Combine(rootedPath(binPath), file);
        }
    }
}