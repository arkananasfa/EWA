using System;
using UnityEngine;

public class BerserkAttacker : FrontAttacker {

    public static int lostHPToAdditionalProjectile = 10;

    public ShowParameter AttackCount;

    public BerserkAttacker(Unit unit, int distance = 2, int times = 1) : base(unit, "FrontAttacker", "Axe", distance, times) {
        AttackCount = new ShowParameter(1, true);
        OnHpInfluenceApplied += CheckAttackCount;
    }

    protected override void Attack(Cage attackCage) {float wait = 0;
        for (int i = 0; i < AttackCount.Value; i++) {
            if (attackCage.Unit != null) {
                AnimationContainer.CreateProjectileWait(owner.Cage, attackCage, owner, attackCage.Unit, owner.Damage, attackProjectileCode, wait);
                wait += 0.15f;
            } else {
                attackCage = attackCage.Front(owner.Team);
                if (attackCage == null || attackCage.Unit == null || attackCage.YDistance(owner.Cage) > distance) {
                    break;
                }
            }
        }
    }

    private void CheckAttackCount(Unit a, Unit d, HPInfluence i) {
        AttackCount.Value = Mathf.Clamp(Mathf.FloorToInt((float)(owner.MaxHP - owner.HP) / lostHPToAdditionalProjectile), 0, 1000) + 1;
    }

}