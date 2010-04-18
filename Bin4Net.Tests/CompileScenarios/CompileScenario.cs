using System.Collections.Generic;

namespace Bin4Net.Tests.CompileScenarios
{
  public class CompileScenario
  {
    private readonly List<string> filesToCompile = new List<string>();
    
    public CompileScenario()
    {
      var more = addFiles();
      if (more != null)
        filesToCompile.AddRange(more);
    }
    
    public CompileScenario(IEnumerable<string> files) : this()
    {
      filesToCompile.AddRange(files);
    }

    protected virtual IEnumerable<string> addFiles()
    {
      return null;
    }

    public string[] FilesToCompile { get { return filesToCompile.ToArray(); } }

    public static CompileScenario operator +(CompileScenario scenario1, CompileScenario scenario2)
    {
      var scenario = new CompileScenario(scenario1.filesToCompile);
      scenario.filesToCompile.AddRange(scenario2.filesToCompile);
      return scenario;
    }

    public static CompileScenario operator +(CompileScenario scenario1, string singleFile)
    {
      var scenario = new CompileScenario(scenario1.filesToCompile);
      scenario.filesToCompile.Add(singleFile);
      return scenario;
    }
  }
}