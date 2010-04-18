using System;
using System.IO;
using System.Reflection;
using Bin4Net.Publish;

namespace Bin4Net
{
  [Serializable]
  public class PublishingOptions
  {
    [NonSerialized]
    private Assembly entryAssembly;
    
    public PublishingOptions(Assembly entryAssembly)
    {
      this.entryAssembly = entryAssembly;
    }

    public PublishingOptions() { }

    /// <summary>
    /// Set to true if the publising process should run in a sperate app domain. This can be important if you still want to do anything wit the
    /// assembly/ies in question after you have performed the publishing process
    /// </summary>
    public bool RunInSeparateAppDomain { get; set; }

    /// <summary>
    /// Set the path to the entry assembly.
    /// </summary>
    public string PathToAssembly { get; set; }

    /// <summary>
    /// Pointer to the contents of the torrent. This may be a file or a directory
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// The name of the torrent. By default it will be set to the title you define in the Bin4Net entry point
    /// </summary>
    public string TorrentName { get; set; }

    /// <summary>
    /// Entry assembly that contains the entry point for Bin4Net
    /// </summary>
    public Assembly EntryAssembly
    {
      get
      {
        return entryAssembly ?? (entryAssembly = identifyEntryAssembly());
      }
    }

    /// <summary>
    /// Set a custom <see cref="IPublisher"/> implementation. Usually useful for testing
    /// </summary>
    public IPublisher CustomPublisher
    {
      get; set;
    }

    

    public void Validate()
    {
      if (Content == null)
        throw new ArgumentException("Content must be set");
      if (entryAssembly == null && PathToAssembly == null)
        throw new ArgumentException("An assembly must be specified that containes the Bin4Net entry point.");
    }

    private Assembly identifyEntryAssembly()
    {
      if (PathToAssembly == null)
        throw new ArgumentNullException("entryAssembly");
      if (!Path.IsPathRooted(PathToAssembly))
        PathToAssembly = Path.Combine(Environment.CurrentDirectory, PathToAssembly);
      if (!File.Exists(PathToAssembly))
        throw new ArgumentException("The entry assembly could not be found with the path " + PathToAssembly);
      return Assembly.LoadFile(PathToAssembly);
    }
  }
}