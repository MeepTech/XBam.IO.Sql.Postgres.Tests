using Meep.Tech.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Meep.Tech.XBam.IO.Sql.Postgres.Tests {
  public class AutoPortTests {

    public static Tester.Result OnLoadFromCache(Universe universe) {
      var (mom, dad, (billy, (bob, joe, _))) = GetTheFishFamily(true);


    }


    public static Tester.Result OnBuildFromCache(Universe universe) {
      var (mom, dad, (billy, (bob, joe, _))) = GetTheFishFamily(true, true);

      if (joe.Mother.Id() != mom.Id()) {
        return new(false, "Mom Not Set Properly");
      }

      if (joe.Father.Id() != dad.Id()) {
        return new(false, "Dad Not Set Properly");
      }

      if (joe.Siblings.Count != 3) {
        return new(false, "All 3 Siblings Not Set");
      }

      if (!joe.Siblings.TryGetValue(billy.Id(), out var biSibling)) {
        return new(false, "Sibling Billy not found!");
      }

      if (biSibling.ToJson().ToString() != billy.ToJson().ToString()) {
        return new(false, "Sibling Billy does not match!");
      }

      if (!joe.Siblings.TryGetValue(bob.Id(), out var boSibling)) {
        return new(false, "Sibling Bob not found!");
      }

      if (boSibling.ToJson().ToString() != bob.ToJson().ToString()) {
        return new(false, "Sibling Bob does not match!");
      }

      return new();
    }

    public static Tester.Result OnLoadFromDb(Universe universe) {
      var (mom, dad, (billy, (bob, joe, _))) = GetTheFishFamily();


    }

    public static Tester.Result OnBuildFromDb(Universe universe) {
      GetTheFishParents(false, out Fish mom, out Fish dad);
      universe.Sql().Insert(mom);
      universe.Sql().Insert(dad);

      Fish billy = GetFishBilly(false,  true, mom, dad);
      universe.Sql().Insert(billy);

      Fish bob = GetFishBob(false, true, mom, dad, billy);
      universe.Sql().Insert(bob);

      Fish joe = GetFishJoe(false, true, mom, dad, billy, bob);
      universe.Sql().Insert(joe);

      if (joe.Mother.Id() != mom.Id()) {
        return new(false, "Mom Not Set Properly");
      }

      if (joe.Father.Id() != dad.Id()) {
        return new(false, "Dad Not Set Properly");
      }

      if (joe.Siblings.Count != 3) {
        return new(false, "All 3 Siblings Not Set");
      }

      if (!joe.Siblings.TryGetValue(billy.Id(), out var biSibling)) {
        return new(false, "Sibling Billy not found!");
      }

      if (biSibling.ToJson().ToString() != billy.ToJson().ToString()) {
        return new(false, "Sibling Billy does not match!");
      }

      if (!joe.Siblings.TryGetValue(bob.Id(), out var boSibling)) {
        return new(false, "Sibling Bob not found!");
      }

      if (boSibling.ToJson().ToString() != bob.ToJson().ToString()) {
        return new(false, "Sibling Bob does not match!");
      }

      return new();
    }

    static List<Fish> GetTheFishFamily(bool setToCache = false, bool useIds = false) {
      GetTheFishParents(setToCache, out Fish mom, out Fish dad);
      Fish billy = GetFishBilly(setToCache, useIds, mom, dad);
      Fish bob = GetFishBob(setToCache, useIds, mom, dad, billy);
      Fish joe = GetFishJoe(setToCache, useIds, mom, dad, billy, bob);

      return new() {
        mom,
        dad,
        billy,
        bob,
        joe
      };
    }

    static Fish GetFishJoe(bool setToCache, bool useIds, Fish mom, Fish dad, Fish billy, Fish bob) {
      return Fish.Types.Get<Carp>()
        .Builder()
          .Add(nameof(Fish.Name), "Joe")
          .Add(nameof(Fish.Mother), useIds ? mom.Id() : mom)
          .Add(nameof(Fish.Father), useIds ? dad.Id() : dad)
          .Add(nameof(Fish.Siblings), useIds
            ? new[] { billy.Id(), bob.Id() }
            : new Fish[] { billy, bob }.ToDictionary(f => f.Id()))
        .Make()
        .ThenIf(setToCache, ICachedExtensions.Cache);
    }

    static Fish GetFishBob(bool setToCache, bool useIds, Fish mom, Fish dad, Fish billy) {
      return Fish.Types.Get<Carp>()
        .Builder()
          .Add(nameof(Fish.Name), "Bob")
          .Add(nameof(Fish.Mother), useIds ? mom.Id() : mom)
          .Add(nameof(Fish.Father), useIds ? dad.Id() : dad)
          .Add(nameof(Fish.Siblings), useIds
            ? billy.Id().AsSingleItemEnumerable()
            : billy.AsSingleItemEnumerable().ToDictionary(f => f.Id()))
        .Make()
        .ThenIf(setToCache, ICachedExtensions.Cache);
    }

    static Fish GetFishBilly(bool setToCache, bool useIds, Fish mom, Fish dad) {
      return Fish.Types.Get<Carp>()
        .Builder()
          .Add(nameof(Fish.Name), "Billy")
          .Add(nameof(Fish.Mother), useIds ? mom.Id() : mom)
          .Add(nameof(Fish.Father), useIds ? dad.Id() : dad)
        .Make()
        .ThenIf(setToCache, ICachedExtensions.Cache);
    }

    static void GetTheFishParents(bool setToCache, out Fish mom, out Fish dad) {
      mom = Fish.Types.Get<Carp>()
        .Builder()
          .Add(nameof(Fish.Name), "Mom")
        .Make()
        .ThenIf(setToCache, ICachedExtensions.Cache);
      dad = Fish.Types.Get<Carp>()
        .Builder()
          .Add(nameof(Fish.Name), "Dad")
        .Make()
        .ThenIf(setToCache, ICachedExtensions.Cache);
    }
  }
}
