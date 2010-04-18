using System;
using System.IO;
using System.Reflection;
using Bin4Net.Inlets;
using Bin4Net.Publish;

namespace Bin4Net
{
  /// <summary>
  /// Start point to the publishing process
  /// </summary>
  public class Publisher
  {
    
    /// <summary>
    /// Call with an assembly already available
    /// </summary>
    public Publisher(PublishingOptions options)
    {
      if (!options.RunInSeparateAppDomain)
      {
        var p = new PublishProcess();
        p.SetData(options);
        p.Start();
        return;
      }

      var exp = new AppDomainExpander<PublishProcess>();
      var setup = new AppDomainSetup { ApplicationBase = Environment.CurrentDirectory };
      exp.Create(setup, options);
      exp.End();
    }
  }
}