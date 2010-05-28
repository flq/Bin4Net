using System;

namespace Bin4Net.Publish
{
    /// <summary>
    /// One-stop shop to publish your assemblies
    /// </summary>
    public interface IPublisher
    {
        /// <summary>
        /// Setup your basic metadata, things like the name of your bin, who publishes it etc.
        /// </summary>
        /// <param name="metadata">Provide a method that accepts a <see cref="IMetadataInlet"/></param>
        /// <returns></returns>
        IPublisher SetupMetadata(Action<IMetadataInlet> metadata);

        /// <summary>
        /// Enter the versioning capabilities.
        /// </summary>
        IPublisher Versioning(Action<IVersioningInlet> versioning);

        IPublisher DependsOn(params string[] dependencies);

        /// <summary>
        /// Specify the Url under which your bin will be available. If your bin consists of several files, 
        /// you should specify the folder that contains them. If you prefer providing your bin as a single zip, specify the 
        /// full path. This method can be called several times to specify multiple download locations.
        /// When you provide the download url, you can provide the following placeholders that will be filled in
        /// appropriately:<br/>
        /// - {EntryAssemblyName} : The name of the entry assembly
        /// </summary>
        IPublisher DownloadUnder(string url);
    }

    public interface IVersioningInlet
    {
        IVersioningInlet VersionOfThisAssembly();
    }

    public interface IMetadataInlet
    {
        IMetadataInlet FromAssemblyAttributes();
        IMetadataInlet AssociateWithTags(params string[] tags);
    }
}
