using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bin4Net.Publishing;

namespace Bin4Net.PublishCLI
{
  class Program
  {
    static void Main(string[] args)
    {
      var showHelp = false;
      var setup = new PublishingOptions();

      var options =
        new OptionSet
          {
            {"t|torrentname=", "The name the torrent file should get. By default this is the title provided by the entry assembly", v => setup.TorrentName = v},
            {"a|assembly=", "path to the assembly that is the entry point", v=> setup.PathToAssembly = v},
            {"s|separate", "assembly loading will be done in a separate appdomain that is unloaded when the publisher finishes", v=> setup.RunInSeparateAppDomain = true},
            {"c|content=", "File or directory that constitutes the content of the download", v=> setup.Content = v},
            {"h|help", "Shows the usage help", v => showHelp = true}
          };
      try
      {
        options.Parse(args);
        if (showHelp)
        {
          options.WriteOptionDescriptions(Console.Out);
          goto end;
        }
        setup.Validate();
        //using (var runner = new BuildRunner(setup))
        //  runner.Run();
      }
      catch (OptionException x)
      {
        Console.WriteLine("The options to the publisher were not understood: {0}", x.Message);
        options.WriteOptionDescriptions(Console.Out);
      }
      catch (ArgumentException x)
      {
        Console.WriteLine("Publisher has isues with the options: {0}", x.Message);
      }
      end: Console.WriteLine("Publisher finished.");
    }
  }
}
