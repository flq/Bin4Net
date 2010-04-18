using System;
using System.Linq;
using System.Reflection;
using Bin4Net.Publish;

namespace Bin4Net
{
  public class EntryPointFinder
  {
    private readonly Assembly assembly;

    public EntryPointFinder(Assembly assembly)
    {
      this.assembly = assembly;
    }

    public void Invoke(IPublisher publisher)
    {
      var result = findEntryPoint();
      // Just for reference, considering we call the method a single time
      //var @delegate = (Action<IPublisher>)Delegate.CreateDelegate(typeof(Action<IPublisher>), result, true);
      result.Invoke(null, new object[] { publisher });
    }

    private MethodInfo findEntryPoint()
    {
      try
      {
        return (from t in assembly.GetTypes()
                from m in
                  t.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                where !m.IsGenericMethod && m.ReturnType == typeof(void)
                let parms = m.GetParameters()
                where
                  parms.Length == 1 &&
                  !parms[0].IsIn && !parms[0].IsOut &&
                  parms[0].ParameterType == typeof(IPublisher)
                select m).Single();
      }
      catch (InvalidOperationException x)
      {
        string errMsg;
        switch (x.Message)
        {
          case "Sequence contains more than one element":
            errMsg = string.Format("Too many entry points found in assembly {0}", assembly.FullName);
            break;
          case "Sequence contains no elements":
            errMsg = string.Format("No Entry point has been found in assembly {0}", assembly.FullName);
            break;
          default:
            errMsg = "Too many or no Bin4Netentry points found, please revise your assembly";
            break;
        }
        throw new EntryPointNotFoundException(errMsg);
      }
    }
  }
}