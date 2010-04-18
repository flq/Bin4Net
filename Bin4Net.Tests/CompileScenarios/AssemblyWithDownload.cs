namespace Bin4Net.Tests.CompileScenarios
{
  public class AssemblyWithDownload : CompileScenario
  {
    protected override System.Collections.Generic.IEnumerable<string> addFiles()
    {
      return (new AttributesOnly() + "EntryPoint3.cs").FilesToCompile;
    }
  }
}