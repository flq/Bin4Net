using System;
using Bin4Net.Publish;
using Bin4Net.PublishCommands;

namespace Bin4Net.Inlets
{
  internal class MetadataInlet : IMetadataInlet
  {
    private readonly PublishInlet publishInlet;

    public MetadataInlet(PublishInlet publishInlet)
    {
      this.publishInlet = publishInlet;
    }

    public IMetadataInlet FromAssemblyAttributes()
    {
      publishInlet.Add(new DataFromAssemblyCommand());
      return this;
    }

    public IMetadataInlet AssociateWithTags(params string[] tags)
    {
      publishInlet.Add(new TagSetupCommand(tags));
      return this;
    }
  }
}