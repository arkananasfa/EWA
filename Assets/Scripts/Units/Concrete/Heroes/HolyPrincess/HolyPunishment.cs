using System.Collections.Generic;

public class HolyPunishment : ActiveSkill {

    public HolyPunishment(Unit unit) : base(unit, "HolyPunishment", new Cooldown(3)) {
        applyEffect += CastPunishment;
    }

    private void CastPunishment(Cage cage) {
        Unit unit = cage.Unit;
        Effect effect = new Effect(unit, "HolyPunishment", 2);
        effect.OnHpInfluenceApplied += (damager, victim, hpInfluence) => {
            if (damager == unit && hpInfluence.Type == HPChangeType.Damage) {
                HPInfluence copy = hpInfluence.Copy();
                copy.DamageType = DamageType.Absolute;
                damager.ApplyHPChange(owner, copy);
            }
        };
    }

    protected override List<Cage> GetPossibleCages() {
        return CageListBuilder.New.UseInRadius(owner.Cage, 2).OnlyWithEnemies(owner.Team).Cages;
    }

}