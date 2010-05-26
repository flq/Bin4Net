using Bin4Net.Publish;
using Bin4Net.Publishing.PublishCommands;

namespace Bin4Net.Publishing.Inlets
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