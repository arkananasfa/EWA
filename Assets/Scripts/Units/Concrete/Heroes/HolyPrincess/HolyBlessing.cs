using System.Collections.Generic;

public class HolyBlessing : ActiveSkill {

    private readonly int HealHerself = 30;
    private readonly int HealOther = 15;

    public HolyBlessing(Unit unit) : base(unit, "HolyBlessing", new Cooldown(1)) {
        applyEffect += Bless;
    }

    private void Bless(Cage cage) {
        Unit unit = cage.Unit;
        if (unit == owner)
            unit.ApplyHPChange(owner, HPInfluence.NewHeal(HealHerself));
        else
            unit.ApplyHPChange(owner, HPInfluence.NewHeal(HealOther));
    }

    protected override List<Cage> GetPossibleCages() {
        return CageListBuilder.New.UseCage(owner.Cage).UseInRadius(owner.Cage, 2).OnlyWithAllies(owner.Team).Cages;
    }

}