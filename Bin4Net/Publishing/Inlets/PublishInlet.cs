using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Bin4Net.Publish;
using Bin4Net.Publishing.PublishCommands;

namespace Bin4Net.Publishing.Inlets
{
  internal class PublishInlet : IPublisher, IPublishCommands
  {
    private readonly List<PublishCommand> commands = new List<PublishCommand>();

    public IPublisher SetupMetadata(Action<IMetadataInlet> metadata)
    {
      var i = new MetadataInlet(this);
      metadata(i);
      return this;
    }

    public IPublisher AssociateWithTags(params string[] tags)
    {
      Add(new TagSetupCommand(tags));
      return this;
    }

    public IPublisher DependsOn(params string[] dependencies)
    {
      return this;
    }

    public IPublisher DownloadUnder(string url)
    {
      Add(new AddWebSeedCommand(url));
      return this;
    }

    ReadOnlyCollection<PublishCommand> IPublishCommands.Commands
    {
      get { return commands.AsReadOnly(); }
    }

    void IPublishCommands.Prepend(PublishCommand command)
    {
      commands.Insert(0, command);
    }

    void IPublishCommands.Append(PublishCommand command)
    {
      commands.Add(command);
    }

    internal void Add(PublishCommand command)
    {
      commands.Add(command);
    }

    IEnumerator<PublishCommand> IEnumerable<PublishCommand>.GetEnumerator()
    {
      return commands.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return ((IEnumerable<PublishCommand>)this).GetEnumerator();
    }
  }
}