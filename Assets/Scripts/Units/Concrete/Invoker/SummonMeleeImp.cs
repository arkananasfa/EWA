using System.Collections.Generic;

public class SummonMeleeImp : SummonSkill {

    protected override UnitType UnitType { get; set; } = UnitType.MeleeImp;

    public SummonMeleeImp(Unit unit) : base(unit, "SummonMeleeImp", new Cooldown(2)) {}

    protected override void Summon(Cage cage) {
        base.Summon(cage);
        owner.ActiveSkills.ForEach(s => s.Cooldown.Use());
    }

    protected override List<Cage> GetPossibleCages() {
        return CageListBuilder.New.Use8Neighbor(owner.Cage).OnlyIf(c => owner.Cage.Y + Game.CurrentTeam.frontDirection == c.Y).Cages;
    }

}