namespace Bin4Net.Tests.CompileScenarios
{
  public class AttributesOnly : CompileScenario
  {
    protected override System.Collections.Generic.IEnumerable<string> addFiles()
    {
      return new [] { "AssemblyInfo1.cs" };
    }
  }
}