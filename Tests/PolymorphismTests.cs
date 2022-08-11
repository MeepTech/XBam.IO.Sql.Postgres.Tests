using System;

namespace Meep.Tech.XBam.IO.Sql.Postgres.Tests {
  public class PolymorphismTests {
    public static Tester.Result SingleJsonDataFieldSavedInChildType(Universe universe) {
      var plant = Plant.Types.Get<Birch>()
        .Builder()
          .Add(nameof(Plant.NumberOfLeaves), 50)
        .Make();

      universe.Sql()
        .Insert(plant);

      var loadedPlant = universe.Sql()
        .Select<Tree>(plant.Id());

      string original = plant
        .ToJson()
        .ToString();

      string loaded = loadedPlant
        .ToJson()
        .ToString();

      Console.WriteLine("Inserted:");
      Console.WriteLine(original);
      Console.WriteLine("================================");
      Console.WriteLine("Selected:");
      Console.WriteLine(loaded);
      Console.WriteLine("================================");

      bool result = original == loaded;
      Console.WriteLine(result);

      return result 
        ? new() 
        : new(false, "Selected Model value does not equal Inserted Model value.");
    }

    public static Tester.Result MultipleJsonDataFieldsSavedInChildType(Universe universe) {
      var plant = Plant.Types.Get<Daisy>()
        .Builder()
          .Add(nameof(Flower.Color), "Green")
          .Add(nameof(Flower.Diameter), 4.5f)
        .Make();

      universe.Sql()
        .Insert(plant);

      var loadedPlant = universe.Sql()
        .Select<Flower>(plant.Id());

      string original = plant
        .ToJson()
        .ToString();

      string loaded = loadedPlant
        .ToJson()
        .ToString();

      Console.WriteLine("Inserted:");
      Console.WriteLine(original);
      Console.WriteLine("================================");
      Console.WriteLine("Selected:");
      Console.WriteLine(loaded);
      Console.WriteLine("================================");

      bool result = original == loaded;
      Console.WriteLine(result);

      return result
        ? new()
        : new(false, "Selected Model value does not equal Inserted Model value.");
    }

    public static Tester.Result NoJsonDataFieldsSavedInParentType(Universe universe) {
      var plant = Plant.Types.Get<Bush>()
        .Builder()
          .Add(nameof(Plant.Name), "Tom")
        .Make();

      universe.Sql()
        .Insert(plant);

      var loadedPlant = universe.Sql()
        .Select<Plant>(plant.Id());

      string original = plant
        .ToJson()
        .ToString();

      string loaded = loadedPlant
        .ToJson()
        .ToString();

      Console.WriteLine("Inserted:");
      Console.WriteLine(original);
      Console.WriteLine("================================");
      Console.WriteLine("Selected:");
      Console.WriteLine(loaded);
      Console.WriteLine("================================");

      bool result = original == loaded;
      Console.WriteLine(result);

      return result
        ? new()
        : new(false, "Selected Model value does not equal Inserted Model value.");
    }
  }
}
