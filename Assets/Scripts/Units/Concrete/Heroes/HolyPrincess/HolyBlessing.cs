using System.Collections.Generic;

public class HolyBlessing : ActiveSkill {

    private readonly int HealHerself = 20;
    private readonly int HealOther = 10;

    public HolyBlessing(Unit unit) : base(unit, "HolyBlessing", new Cooldown(1)) {
        applyEffect += Bless;
    }

    private void Bless(Cage cage) {
        Unit unit = cage.Unit;
        UnitView unitView = unit.View;
        if (unit == owner) {
            unit.ApplyHPChange(owner, HPInfluence.NewHeal(HealHerself));
            soundNumber = 0;
        } else {
            unit.ApplyHPChange(owner, HPInfluence.NewHeal(HealOther));
            soundNumber = 1;
        }
        unit.UseDispel(Effect.Power.Weak, Effect.DispelType.Bad);
        unitView.UpdateStatus();
    }

    protected override List<Cage> GetPossibleCages() {
        return CageListBuilder.New.UseCage(owner.Cage).UseInRadius(owner.Cage, 2).OnlyWithAllies(owner.Team).Cages;
    }

}