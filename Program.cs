using Meep.Tech.XBam.Configuration;
using System;

namespace Meep.Tech.XBam.IO.Sql.Postgres.Tests {
  internal class Program {

    static void Main(string[] args) {
      Universe universe = InitializeXBam(
        "Server=\"192.168.1.13\";" 
          + "Port=\"5432\";" 
          + "Database=\"XBamTesting\";" 
          + "User Id=\"xbam-tester\";" 
          + "Password=\"test\";"
      );

      Tester tester = new(universe);

      tester.Run(
        "Polymorphic Parent Class; Insert and Select are Equal (Bush)",
        PolymorphismTests.NoJsonDataFieldsSavedInParentType
      );

      tester.Run(
        "Polymorphic Parent Class With Single Additional Field; Insert and Select are Equal (Birch)",
        PolymorphismTests.SingleJsonDataFieldSavedInChildType
      );

      tester.Run(
        "Polymorphic Child Class With Multiple Additional Fields; Insert and Select are Equal (Daisy)",
        PolymorphismTests.MultipleJsonDataFieldsSavedInChildType
      );
    }

    public static Universe InitializeXBam(string connectionString, Loader.Settings options = null) {
      options ??= new();
      options.FatalOnCannotInitializeType = true;
      options.PreLoadAssemblies.Add(typeof(Program).Assembly);
      Universe universe = new(new(options));

      universe.SetExtraContext<ConsoleProgressLogger>(new(verboseModeForNonErrors: true));

      universe.SetExtraContext(
        new PostgreSqlContext(
          new PostgreSqlContext.Settings(
            connectionString
          ) {
            UseJsonFieldsForUnknownColumnDataTypes = true,
            DropAllTablesBeforeCreatingThem = true
          }
        )
      );

      universe.Loader.Initialize();
      return universe;
    }
  }
}
