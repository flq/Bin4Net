using System;

namespace Bin4Net.Publish
{
    /// <summary>
    /// Standards: Use metadata from available attributes and use version of the assembly containing the bin4net entry
    /// point
    /// </summary>
    public class Standards : IPublisherAutomaton
    {
        public void Accept(IPublisher publisher)
        {
            publisher
                .SetupMetadata(m => m.FromAssemblyAttributes())
                .Versioning(v => v.VersionOfThisAssembly());
        }
    }
}