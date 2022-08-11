using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meep.Tech.XBam.IO.Sql.Postgres.Tests {
  public class Tester {

    public record Result (bool Success = true, string Message = null);

    public Tester(Universe universe) {
      Universe = universe;
    }

    public Universe Universe { get; }

    public void Run(string name, Func<Universe, Result> test) {
      Console.WriteLine($"Running Test: {name}, on Universe: {Universe.Key}");
      try {
        var result = test(Universe) ?? new();
        if (result.Success) {
          Console.WriteLine($"\t- Test Succeeded: \n\t\t{result.Message ?? "Success!"}");
        } else {
          Console.Error.WriteLine($"\t- Test Failed: \n\t\t{result.Message ?? "Failure!!!"}");
        }
      }
      catch (Exception ex) {
        Console.Error.WriteLine($"\t- Test Failed: {name}: Uncaught Exception:");
        Console.WriteLine(ex.ToString().Replace(Environment.NewLine, "\n\t\t"));
      }
    }
  }
}
