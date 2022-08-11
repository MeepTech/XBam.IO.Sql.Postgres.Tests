namespace Meep.Tech.XBam.IO.Sql.Postgres.Tests {

  /// <summary>
  /// Base model for plants of type tree
  /// </summary>
  public class Tree : Plant {

    public enum BarkColor {
      Brown,
      Tan,
      Blue,
      Green,
      White
    }

    [AutoBuild(FromDefaultOnly = true)]
    public BarkColor BarkType {
      get;
      private set;
    }

    [Branch]
    public new abstract class Type : Plant.Type {

      public abstract BarkColor DefaultBarkType 
        { get; }

      /// <summary>
      /// Used to make new Child Archetypes for Tree.Type 
      /// </summary>
      /// <param name="id">The unique identity of the Child Archetype</param>
      protected Type(Identity id)
        : base(id) { }
    }
  }
}
