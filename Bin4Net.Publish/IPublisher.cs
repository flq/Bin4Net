using System;

namespace Bin4Net.Publish
{
    /// <summary>
    /// One-stop shop to publish your assemblies. The three main things to consider is to define
    /// - The publisher
    /// - The product
    /// - The version
    /// Furthermore you should specify at least one download location as the initial point to obtain
    /// your bin.
    /// </summary>
    public interface IPublisher
    {
        /// <summary>
        /// Apply an <see cref="IPublisherAutomaton"/> to reuse standard settings. The
        /// bin4net already contains the <see cref="Standards"/> automaton.
        /// </summary>
        IPublisher ApplyAutomaton<T>() where T : IPublisherAutomaton, new();

        /// <summary>
        /// Setup your basic metadata, things like the name of your bin, who publishes it etc.
        /// </summary>
        IPublisher SetupMetadata(Action<IMetadataInlet> metadata);

        /// <summary>
        /// Enter the versioning capabilities.
        /// </summary>
        IPublisher Versioning(Action<IVersioningInlet> versioning);


        /// <summary>
        /// Enter the dependecy definition capabilities.
        /// </summary>
        IPublisher DependsOn(Action<IDependencyInlet> dependencies);

        /// <summary>
        /// Enter the Download location definition capabilities.
        /// </summary>
        IPublisher Download(Action<IDownloadInlet> download);
    }

    /// <summary>
    /// State from where your bin is available
    /// </summary>
    public interface IDownloadInlet
    {
        /// <summary>
        /// Define a fixed location for your bin (e.g. http://github.com/flq/downloads/). If your bin is a single file,
        /// you may point to that file, otherwise ensure that your URL ends with a slash. That way, a multi-file bin
        /// will be escaped relative to the URL you specify.
        /// </summary>
        IDownloadInlet FixedLocation(string url);
    }

    /// <summary>
    /// State which dependencies your bin has to other bins.
    /// </summary>
    public interface IDependencyInlet
    {
        /// <summary>
        /// State a dependency of the bin. Use the static "From" methods of the <see cref="DependencyDefinition"/>
        /// class to define the required instance
        /// </summary>
        IDependencyInlet Bin(DependencyDefinition dependencyDefinition);
    }

    /// <summary>
    /// define the version of your bin
    /// </summary>
    public interface IVersioningInlet
    {
        /// <summary>
        /// State that the version of your bin is defined by the version of the assembly in which the bin4net entry point
        /// is defined
        /// </summary>
        IVersioningInlet VersionOfThisAssembly();

        //TODO: Compatibility strategy to state if you stay compatible on major/minor/revisions
    }

    /// <summary>
    /// define the metadata of your bin
    /// </summary>
    public interface IMetadataInlet
    {
        /// <summary>
        /// Advice bin4net to extract relevant metadata from the standard assembly attributes
        /// </summary>
        IMetadataInlet FromAssemblyAttributes();

        /// <summary>
        /// Associate your bin with tags that will help consumers of your bin to categorize it.
        /// This setting influences the size of the torrent file.
        /// </summary>
        IMetadataInlet AssociateWithTags(params string[] tags);
    }
}
