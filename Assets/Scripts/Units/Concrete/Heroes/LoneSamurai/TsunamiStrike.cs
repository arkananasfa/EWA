using System.Collections.Generic;
using UnityEngine;

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
                    AnimationContainer.CreateProjectile(defender.Cage, unit.Cage, owner, unit, owner.Damage, "SamuraiSword");
                } else {
                    AnimationContainer.CreateProjectile(defender.Cage, unit.Cage, owner, unit,
                                                        HPInfluence.NewDamage(owner.Damage.Value / noDeathPromiseDamageReduction, DamageType.Physical, RangeType.Melee),
                                                        "SamuraiSword"
                                                        );
                }
            }
            Cooldown.Refresh();
        }
    }
    
}