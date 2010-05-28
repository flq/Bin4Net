using System;

namespace Bin4Net.Publishing.PublishCommands
{
    public class VersionFromAssemblyCommand : PublishCommand
    {
        protected override void execute()
        {
            var v = context.EntryAssembly.GetName().Version;
            context.Seed.Version = v.ToString();
        }
    }
}