using Meep.Tech.Noise;

namespace Meep.Tech.XBam.IO.Sql.Postgres.Tests {
  public class Birch : Tree.Type {

    public override Tree.BarkColor DefaultBarkType
      => Tree.BarkColor.White;

    public override string Name
      => nameof(Birch);

    public override int DefaultNumberOfLeaves
      => RNG.Static.Next(10, 100);

    public override float DefaultHeight
      => RNG.Static.Next(10, 100);

    Birch()
      : base(new Birch.Identity(nameof(Birch))) { }
  }
}
