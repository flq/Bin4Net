using System;
using System.Collections.Generic;
using System.Reflection;

namespace Bin4Net.Publishing.PublishCommands
{
  internal class DataFromAssemblyCommand : PublishCommand
  {

    private readonly Dictionary<Type, Action<PublishedSeed, Attribute>> seedAction =
      new Dictionary<Type, Action<PublishedSeed, Attribute>>
        {
          { typeof(AssemblyCompanyAttribute), (s, a) => s.Publisher = ((AssemblyCompanyAttribute)a).Company },
          { typeof(AssemblyCopyrightAttribute), (s, a) => s.Copyright = ((AssemblyCopyrightAttribute)a).Copyright },
          { typeof(AssemblyProductAttribute), (s, a) =>
                                                  {
                                                      s.Product = ((AssemblyProductAttribute) a).Product;
                                                      s.TorrentName = ((AssemblyProductAttribute) a).Product;
                                                  }}
        };

    protected override void execute()
    {
      var attributes = context.EntryAssembly.GetCustomAttributes(false);
      var seed = context.Seed;
      foreach (var a in attributes)
      {
        Action<PublishedSeed, Attribute> action;
        seedAction.TryGetValue(a.GetType(), out action);
        if (action != null)
          action(seed, (Attribute)a);
      }
    }
  }
}