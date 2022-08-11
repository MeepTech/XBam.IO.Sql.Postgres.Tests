using Meep.Tech.Noise;

namespace Meep.Tech.XBam.IO.Sql.Postgres.Tests {

  [Branch(typeof(Flower))]
  public class Daisy : Plant.Type {

    public override string Name
      => nameof(Daisy);

    public override int DefaultNumberOfLeaves
      => RNG.Static.Next(0, maxValue: 3);

    public override float DefaultHeight
      => (float)RNG.Static.NextDouble().Clamp(0.1, 2);

    public string DefaultColor 
      => "White";

    Daisy() 
      : base(null) {}
  }
}
