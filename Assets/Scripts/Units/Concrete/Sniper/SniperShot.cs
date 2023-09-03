using System;
using System.Collections.Generic;

public class SniperShot : ActiveSkill {

    public SniperShot(Unit unit) : base(unit, "SniperShot", new Cooldown(2, 1)) {
        applyEffect += CastSniperShot;
    }

    private void CastSniperShot(Cage target) {
        AnimationSequence.New().AddMain(AnimatedObject.CreateAttackProjectile(owner.Cage, target, "Bullet", 4000f));
        target.Unit.ApplyHPChange(owner, owner.Damage);
    }

    protected override List<Cage> GetPossibleCages() {
        List<Cage> targets = new();
        int maxDistance = 0;
        foreach (var unit in owner.Team.Opponent.Units) {
            int distance = unit.Cage.Distance(owner.Cage);
            if (distance > maxDistance) {
                targets.Clear();
                targets.Add(unit.Cage);
                maxDistance = distance;
            } else if (distance == maxDistance) {
                targets.Add(unit.Cage);
            }
        }
        return targets;
    }

}