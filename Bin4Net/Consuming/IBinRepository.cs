using MonoTorrent.Client;

namespace Bin4Net.Consuming
{
  internal interface IBinRepository
  {
      /// <summary>
      /// Get a binary package by providing a path of publisher/product/version.
      /// </summary>
      Bin Get(string binPath);

      /// <summary>
      /// The root of the repository
      /// </summary>
      string Root { get; }

      /// <summary>
      /// Places the torrent at its correct location
      /// </summary>
      void Put(BinTorrent bt);
  }
}