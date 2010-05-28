using System;
using System.Collections.Generic;
using System.IO;
using Bin4Net.Consuming;
using MonoTorrent.Client.PieceWriters;

namespace Bin4Net.Tests.Util
{
    public class TestingBinRepository : IBinRepository, IDisposable
    {
        private string root;

        public TestingBinRepository()
        {
            var tmp = Path.GetTempPath();
            var dir = Path.GetRandomFileName();
            root = Path.Combine(tmp, dir);
            Directory.CreateDirectory(root);
        }

        public Bin Get(string binPath)
        {
            return null;
        }

        public string Root { get { return root; } }

        public void Dispose()
        {
            Directory.Delete(root, true);
        }
    }
}