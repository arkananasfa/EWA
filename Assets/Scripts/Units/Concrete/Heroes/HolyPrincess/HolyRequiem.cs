using System.Collections.Generic;

public class HolyRequiem : ActiveSkill {

    public HolyRequiem(Unit unit) : base(unit, "HolyRequiem", new Cooldown(7)) {
        applyEffect += CastRequiem;
    }

    private void CastRequiem(Cage cage) {
        Unit unit = cage.Unit;
        Effect effect = new Effect(unit, "HolyRequiem", 3);
        effect.OnPreApplyHpInfluence += (damager, victim, hpInfluence) => {
            if (victim == unit && hpInfluence.Type == HPChangeType.Damage) {
                hpInfluence.IsBlocked = true;
            }
        };
    }

    protected override List<Cage> GetPossibleCages() {
        return CageListBuilder.New.UseCage(owner.Cage).UseInRadius(owner.Cage, 3).OnlyWithAllies(owner.Team).Cages;
    }

}