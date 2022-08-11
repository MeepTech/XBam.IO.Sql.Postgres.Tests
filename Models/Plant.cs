namespace Meep.Tech.XBam.IO.Sql.Postgres.Tests {

  /// <summary>
  /// The Base Model for all Plants
  /// </summary>
  public class Plant : Model<Plant, Plant.Type>, IModel.IUseDefaultUniverse, IUnique {

    string IUnique.Id { get; set; }

    [AutoBuild]
    public string Name {
      get;
      private set;
    }

    [AutoBuild]
    public int NumberOfLeaves {
      get;
      private set;
    }

    [AutoBuild]
    public float Height {
      get;
      private set;
    }

    /// <summary>
    /// The Base Archetype for Plants
    /// </summary>
    public abstract class Type : Archetype<Plant, Plant.Type>.WithAllDefaultModelBuilders {

      public abstract string Name
        { get; }

      public virtual string DefaultName
        => Name;

      public abstract int DefaultNumberOfLeaves
        { get; }

      public abstract float DefaultHeight 
        { get; }

      /// <summary>
      /// Used to make new Child Archetypes for Plant.Type 
      /// </summary>
      /// <param name="id">The unique identity of the Child Archetype</param>
      protected Type(Identity id)
        : base(id) { }
    }
  }
}
