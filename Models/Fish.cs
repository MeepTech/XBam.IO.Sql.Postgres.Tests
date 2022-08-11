using Meep.Tech.Collections.Generic;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Meep.Tech.XBam.IO.Sql.Postgres.Tests {
  public class Fish : Model<Fish, Fish.Type>, ICached<Fish>, IModel.IUseDefaultUniverse {
    Dictionary<string, Fish> _siblings = new();

    string IUnique.Id { get; set; }

    [AutoBuild, Required, NotNull]
    public string Name {
      get;
      private set;
    }

    [AutoBuild]
    public string Color {
      get;
      private set;
    }

    [AutoBuild, AutoPort]
    public Fish Father {
      get;
      private set;
    }

    [AutoBuild, AutoPort]
    public Fish Mother {
      get;
      private set;
    }

    [AutoBuild(FromDefaultOnly = true)]
    public Plant.Type FavoriteFood {
      get;
      private set;
    }

    [AutoBuild, AutoPort]
    public IReadOnlyDictionary<string, Fish> Siblings {
      get => _siblings;
      private set => _siblings = value.ToDictionary(s => s.Key, s => {
        s.Value._siblings[this.Id()] = this;
        _siblings.Values.ForEach(o => o._siblings[s.Key] = s.Value);

        return s.Value;
      });
    }

    public abstract class Type : Archetype<Fish, Type>.WithAllDefaultModelBuilders {

      protected abstract string DefaultColor {
        get;
      }

      protected abstract Plant.Type DefaultFavoriteFood {
        get;
      }

      protected Type(Identity id)
        : base(id) { }
    }
  }
}
