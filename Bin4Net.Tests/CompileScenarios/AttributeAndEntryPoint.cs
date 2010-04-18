namespace Bin4Net.Tests.CompileScenarios
{
  public class AttributeAndEntryPoint : CompileScenario
  {
    protected override System.Collections.Generic.IEnumerable<string> addFiles()
    {
      return (new AttributesOnly() + "EntryPoint1.cs").FilesToCompile;
    }
  }
}