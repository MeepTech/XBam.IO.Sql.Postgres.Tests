using Meep.Tech.Noise;

namespace Meep.Tech.XBam.IO.Sql.Postgres.Tests {
  public class Bush : Plant.Type {

    public override string Name 
      => nameof(Bush);

    public override int DefaultNumberOfLeaves 
      => RNG.Static.Next(5, 304);

    public override float DefaultHeight 
      => (float)RNG.Static.NextDouble().Clamp(0.5, 10);

    Bush() 
      : base(new(nameof(Bush))) {}
  }
}
