using System.Collections.Generic;
using static UnityEngine.GraphicsBuffer;

public class TsunamiStrike : Skill {

    private const int noDeathPromiseDamageReduction = 4;

    public TsunamiStrike(Unit unit) : base(unit, "TsunamiStrike", new Cooldown(1)) {
        OnHpInfluenceApplied += ApplyTsunamiStrike;
    }

    private void ApplyTsunamiStrike(Unit attacker, Unit defender, HPInfluence hpInfluence) {
        if (!CanUse())
            return;

        if (hpInfluence.Type == HPChangeType.Damage && attacker == owner) {
            CageListBuilder builder = CageListBuilder.New.Use4Neighbor(defender.Cage);
            List<Unit> enemiesAttacked = new();
            foreach (var cage in builder.Cages) {
                if (!cage.IsEmpty && cage.Unit.Team != attacker.Team) {
                    enemiesAttacked.Add(cage.Unit);
                }
            }
            Cooldown.Use();
            foreach (var unit in enemiesAttacked) {
                if (unit.HasEffect("DeathPromise")) {
                    AnimationSequence.Add(AnimatedObject.CreateInSequenceAttackProjectile(defender.Cage, unit.Cage, "SamuraiSword"),
                    () => unit.ApplyHPChange(owner, owner.Damage));
                } else {
                    AnimationSequence.Add(AnimatedObject.CreateInSequenceAttackProjectile(defender.Cage, unit.Cage, "SamuraiSword"),
                    () => unit.ApplyHPChange(owner, HPInfluence.NewDamage(owner.Damage.Value / noDeathPromiseDamageReduction, DamageType.Physical, RangeType.Melee)));
                }
            }
            Cooldown.Refresh();
        }
    }
    
}