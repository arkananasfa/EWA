using System.Collections.Generic;

public class SummonRangedImp : SummonSkill {

    protected override UnitType UnitType { get; set; } = UnitType.RangedImp;

    public SummonRangedImp(Unit unit) : base(unit, "SummonRangedImp", new Cooldown(2)) {}

    protected override void Summon(Cage cage) {
        base.Summon(cage);
        owner.ActiveSkills.ForEach(s => s.Cooldown.Use());
    }

    protected override List<Cage> GetPossibleCages() {
        return CageListBuilder.New.Use4Neighbor(owner.Cage).OnlyIf(c => owner.Cage.Y == c.Y).Cages;
    }

}