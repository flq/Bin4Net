using System;
using System.IO;
using Bin4Net.Consuming;

namespace Bin4Net.Tests.Util
{
    public class TestingBinRepository : FileBinRepository, IDisposable
    {

        public TestingBinRepository()
            : base(Path.Combine(Path.GetTempPath(), Path.GetRandomFileName()))
        {
        }

        public void Dispose()
        {
            Directory.Delete(Root, true);
        }
    }
}