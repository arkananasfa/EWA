using System.Collections.Generic;

public class FrontAttacker : BaseUnitAttacker {

    public FrontAttacker(Unit unit, int distance = 1, int times = 1) : base(unit,
                         unit.Damage.RangeType == RangeType.Melee ? "FrontAttacker" : "FrontAttackerRanged",
                         distance,
                         new Cooldown(1, 1)) {
    }

    public FrontAttacker(Unit unit, string code, int distance = 1, int times = 1) : base(unit,
                         code,
                         distance,
                         new Cooldown(1, 1)) {
    }

    protected override List<Cage> GetPossibleCages() {
        for (int i = 1; i <= distance; i++) {
            Cage c = owner.Cage.Front(owner, i);
            if (c == null)
                return new List<Cage>();
            if (!c.IsEmpty && !c.Unit.IsTeammate(owner))
                return new List<Cage>() { owner.Cage.Front(owner, i) };
        }
        return new List<Cage>();
    }



}