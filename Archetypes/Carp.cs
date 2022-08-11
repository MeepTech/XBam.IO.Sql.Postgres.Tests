
namespace Meep.Tech.XBam.IO.Sql.Postgres.Tests {
  public class Carp : Fish.Type {
    protected override string DefaultColor
      => "Red";

    protected override Plant.Type DefaultFavoriteFood
      => Plant.Types.Get<Daisy>();

    Carp()
      : base(null) { }
  }
}
