using System.Collections.Generic;

public class MagicWeakening : ActiveSkill {

    public MagicWeakening(Unit unit) : base(unit, "MagicWeakening", new Cooldown(3, 1)) {
        applyEffect += Cast;
    }

    private void Cast(Cage cage) {
        Unit unit = cage.Unit;
        unit.TryRemoveEffect("MagicWeakening");
        Effect effect = new Effect(unit, "MagicWeakening", 2, Effect.Power.Weak, Effect.Purpose.Bad).WithVisual();
        effect.OnPreApplyHpInfluence += (damager, victim, hpInfluence) => {
            if (damager == unit && hpInfluence.Type == HPChangeType.Damage && hpInfluence.DamageType == DamageType.Magical) {
                hpInfluence.Value /= 2m;
                effect.UseVisual();
            }
        };
    }

    protected override List<Cage> GetPossibleCages() {
        return CageListBuilder.New.UseInRadius(owner.Cage, 3).OnlyWithEnemies(owner.Team).OnlyIf(c => c.Unit.Damage.DamageType == DamageType.Magical).Cages;
    }

}