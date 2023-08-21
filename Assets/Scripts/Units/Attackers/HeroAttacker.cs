using System.Collections.Generic;

public class HeroAttacker : BaseUnitAttacker {

    public HeroAttacker(Unit unit, string attackCode, int distance = 1, int times = 1) : base(unit,
                         "HeroAttacker",
                         distance,
                         new Cooldown(1, 1),
                         attackCode) {
    }

    public HeroAttacker(Unit unit, string code, string attackCode, int distance = 1, int times = 1) : base(unit,
                         code,
                         distance,
                         new Cooldown(1, 1),
                         attackCode) {
    }

    protected override List<Cage> GetPossibleCages() {
        return CageListBuilder.New.UseInRadius(owner.Cage, distance).OnlyWithEnemies(owner.Team).Cages;
    }



}
