namespace Meep.Tech.XBam.IO.Sql.Postgres.Tests {
  public class Flower : Plant {

    [AutoBuild]
    public float Diameter {
      get;
      private set;
    } = 3.5f;

    [AutoBuild]
    public int NumberOfPetals {
      get;
      private set;
    } = 10;

    [AutoBuild]
    public string Color {
      get;
      private set;
    } = "Red";
  }
}
