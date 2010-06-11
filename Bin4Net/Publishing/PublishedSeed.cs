using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using MonoTorrent.BEncoding;
using MonoTorrent.Common;
using System.Linq;

namespace Bin4Net.Publishing
{
    public class PublishedSeed : IPublishCommand
    {
        private readonly TorrentCreator torrent;

        private string torrentName;
        private readonly string pathToContent;
        private string publisher;
        private string copyright;
        private string product;
        private readonly List<string> webSeeds = new List<string>();
        private string version;

        public PublishedSeed() : this(null, null) { }

        public PublishedSeed(string torrentName, string pathToContent)
        {
            torrent = new TorrentCreator();
            //torrent.Announces.Add(new List<string> { "http://tracker.openbittorrent.com/announce" });
            Torrent.CreatedBy = "Bin4Net";
            this.torrentName = torrentName;
            this.pathToContent = pathToContent;
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
            }
        }

        public string Copyright
        {
            get { return copyright; }
            set
            {
                copyright = value;
                Torrent.AddCustom(new BEncodedString("copyright"), new BEncodedString(copyright));
            }
        }

        public string Publisher
        {
            get
            {
                return publisher;
            }
            set
            {
                publisher = value;
                Torrent.Publisher = publisher;
            }
        }

        public string Product
        {
            get
            {
                return product;
            }
            set
            {
                product = value;
                Torrent.AddCustom(new BEncodedString("product"), new BEncodedString(product));
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

        public string Version
        {
            get { return version; }
            set
            {
                version = value;
                Torrent.AddCustom(new BEncodedString("version"), new BEncodedString(version));
            }
        }

        public TorrentCreator Torrent
        {
            get { return torrent; }
        }

        public void Execute(ExecutionContext context)
        {
            if (string.IsNullOrEmpty(pathToContent))
                throw new EmptyBinException("The path to the bin is not set");
            if (webSeeds.Count == 1)
                Torrent.AddCustom(new BEncodedString("url-list"), new BEncodedString(webSeeds[0]));
            else if (webSeeds.Count > 1)
                Torrent.AddCustom(new BEncodedString("url-list"), new BEncodedList(webSeeds.Select(s => new BEncodedString(s)).ToArray()));
            Torrent.Create(new TorrentFileSource(pathToContent), torrentName);
        }
    }
}