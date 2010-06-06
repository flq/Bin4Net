namespace Bin4Net.Tests.CompileScenarios
{
  public class AssemblyWithSingleDownload : CompileScenario
  {
    protected override System.Collections.Generic.IEnumerable<string> addFiles()
    {
        return (new AttributesOnly() + "EntryPointWithSingleItem.cs").FilesToCompile;
    }
  }
}