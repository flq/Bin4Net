using System;
using Bin4Net.Publishing.Inlets;
using Bin4Net.Publishing.PublishCommands;

namespace Bin4Net.Publishing
{
    internal class PublishProcess : DomainLifetimeHook
    {
        internal override void Start()
        {
            var options = (PublishingOptions)data;
            var assembly = options.EntryAssembly;
            if (assembly == null)
                throw new ArgumentNullException("assembly");
            var f = new EntryPointFinder(assembly);
            var publisher = options.CustomPublisher ?? new PublishInlet();
            f.Invoke(publisher);
            var context = new ExecutionContext(options.EntryAssembly, new PublishedSeed(options.TorrentName, options.Content));
            var commands = publisher as IPublishCommands;
            if (commands == null) return;

            commands.Append(context.Seed);

            foreach (var cmd in commands)
                cmd.Execute(context);

        }

        internal override void Stop()
        {

        }
    }
}