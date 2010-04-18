using System;
using Bin4Net.Inlets;

namespace Bin4Net
{
  internal class PublishProcess : DomainLifetimeHook
  {
    internal override void Start()
    {
      var options = (PublishingOptions) data;
      var assembly = options.EntryAssembly;
      if (assembly == null)
        throw new ArgumentNullException("assembly");
      var f = new EntryPointFinder(assembly);
      var publisher = options.CustomPublisher ?? new PublishInlet();
      f.Invoke(publisher);
      var context = new ExecutionContext(options.EntryAssembly, new PublishedSeed(options.TorrentName, options.Content));
      var commands = publisher as IPublishCommands;
      if (commands == null) return;

      foreach (var cmd in commands)
        cmd.Execute(context);
      context.Seed.Finish();
    }

    internal override void Stop()
    {
      
    }
  }
}