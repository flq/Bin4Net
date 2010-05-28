using System;
using System.IO;
using MonoTorrent.Client;

namespace Bin4Net.Consuming
{
  public class Downloader
  {
    private readonly IBinRepository binRepository;

    public Downloader(IBinRepository binRepository)
    {
      this.binRepository = binRepository;
    }

    /// <summary>
    /// Gets the bin by providing a path to an available torrent file
    /// </summary>
    public void GetBin(string torrentFile)
    {
      if (!File.Exists(torrentFile))
          throw new ArgumentException("torrent " + torrentFile + " could not be found for loading.");
        var engine = new ClientEngine(new EngineSettings());

    }

    /// <summary>
    /// Get the bin by specifying the relevant torrent. Use this overload
    /// to get the Torrent from a URL
    /// </summary>
    public void GetBin(Uri torrentFile)
    {

    }
  }
}