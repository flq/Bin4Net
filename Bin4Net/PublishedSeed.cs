using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization;
using MonoTorrent.BEncoding;
using MonoTorrent.Common;
using System.Linq;

namespace Bin4Net
{
  public class PublishedSeed
  {
    private readonly TorrentCreator torrent;

    private string torrentName;
    private string publisher;
    private string copyright;
    private string name;
    private readonly List<string> webSeeds = new List<string>();

    public PublishedSeed() : this(null, null) {}

    public PublishedSeed(string torrentName, string pathToContent)
    {
      torrent = new TorrentCreator();
      torrent.Announces.Add(new List<string> { "http://tracker.openbittorrent.com/announce" });
      torrent.CreatedBy = "Bin4Net";
      this.torrentName = torrentName;
      torrent.Path = pathToContent;
    }

    
    public string TorrentName
    {
      get { return torrentName; }
      set
      {
        //Has already been set through the constructor, this shall have precedence
        if (torrentName != null) 
          return;
        if (value == null)
          throw new ArgumentNullException("torrentName", "The torrent name may not be null");
        torrentName = value;
        if (!Path.HasExtension(torrentName))
          torrentName += ".torrent";
        if (!Path.IsPathRooted(torrentName))
          Path.Combine(Environment.CurrentDirectory, torrentName);
      }
    }

    public string Publisher
    {
      get {
        return publisher;
      }
      set {
        publisher = value;
        torrent.Publisher = publisher;
      }
    }
    
    public string  Copyright
    {
      get { return copyright; }
      set
      {
        copyright = value;
        torrent.AddCustom(new BEncodedString("copyright"), new BEncodedString(torrentName));
      }
    }

    public string Name
    {
      get {
        return name;
      }
      set {
        name = value;
        torrent.AddCustom(new BEncodedString("uniquename"), new BEncodedString(name));
      }
    }

    public void AddWebSeed(string url)
    {
      webSeeds.Add(url);
    }

    public ReadOnlyCollection<string> WebSeeds
    {
      get { return webSeeds.AsReadOnly(); }
    }

    public void Finish()
    {
      if (string.IsNullOrEmpty(torrent.Path))
        throw new EmptyBinException("The path to the bin is not set");
      if (webSeeds.Count == 1)
        torrent.AddCustom(new BEncodedString("url-list"), new BEncodedString(webSeeds[0]));
      else if (webSeeds.Count > 1)
        torrent.AddCustom(new BEncodedString("url-list"), new BEncodedList(webSeeds.Select(s=>new BEncodedString(s)).ToArray()));
      torrent.Create(torrentName);
    }
  }
}