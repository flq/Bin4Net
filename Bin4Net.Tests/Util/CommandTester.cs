using System;
using Bin4Net.PublishCommands;
using Bin4Net.Tests.CompileScenarios;

namespace Bin4Net.Tests.Util
{
  public class CommandTester
  {
    private readonly TestsCompiler tC = new TestsCompiler();
    private PublishedSeed seed;


    public PublishedSeed Seed
    {
      get { return seed; }
    }

    public CommandTester UsingAssemblyFrom<T>() where T : CompileScenario, new()
    {
      tC.With<T>();
      return this;
    }

    internal CommandTester Test<C>() where C : PublishCommand, new()
    {
      return Test(new C());
    }

    internal CommandTester Test(PublishCommand cmd)
    {
      seed = new PublishedSeed();
      var context = new ExecutionContext(tC.Assembly, seed);
      cmd.Execute(context);
      return this;
    }
  }
}