namespace Bin4Net.Tests.CompileScenarios
{
  public class MultiEntryPoint : CompileScenario
  {
    protected override System.Collections.Generic.IEnumerable<string> addFiles()
    {
        return new[] {"AssemblyInfo2.cs", "EntryPointMultiFiles.cs"};
    }
  }

  public class UtilityAssembly : CompileScenario
  {
      protected override System.Collections.Generic.IEnumerable<string> addFiles()
      {
          return new[] { "Utility.cs" };
      }
  }
}