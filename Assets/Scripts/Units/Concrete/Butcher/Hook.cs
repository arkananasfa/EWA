using System;
using System.Collections.Generic;

public class Hook : ActiveSkill {

    public Hook(Unit unit) : base(unit, "Hook", new Cooldown(3, 1)) {
        applyEffect += CastHook;
    }

    private void CastHook(Cage target) {
        var (x, y) = owner.Cage.Difference(target);
        x = Math.Sign(x);
        y = Math.Sign(y);
        AnimationSequence.New().AddMain(AnimatedObject.CreateAt(owner.View)
                                       .SetSkillIconAsSprite("Hook")
                                       .SetSpeed(1400f)
                                       .MoveDefineTime(target.View).AfterThat()
                                       .ReleaseSequence()
                                       .MoveDefineTime(owner.Cage.GetCageIn(x, y).View)
                                       .MoveUnit(target.Unit.View, owner.Cage.GetCageIn(x, y).View)
                                       .UpdateUnitStatus(target.Unit.View)
                                       .AfterThat()
                                       .MoveDefineTime(owner.Cage.View)
                                       .Kill()
        );
        if (owner.Team != target.Unit.Team)
            target.Unit.ApplyHPChange(owner, owner.Damage);
        if (!target.IsEmpty)
            target.Unit.Cage = owner.Cage.GetCageIn(x, y);
    }

    protected override List<Cage> GetPossibleCages() {
        List<Cage> targets = new();
        for (int y = -1;y<2;y++) {
            for (int x = -1;x<2;x++) {
                if (x == 0 && y == 0) continue;
                Cage cage = owner.Cage;
                do {
                    cage = cage.GetCageIn(x, y);
                } while (cage != null && cage.IsEmpty);
                if (cage != null) {
                    targets.Add(cage);
                }
            }
        }
        return targets;
    }

}