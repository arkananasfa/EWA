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
        Cage to = owner.Cage.GetCageIn(x, y);
        UnitView enemyView = target.Unit.View;
        CageView ownerCageView = owner.Cage.View;
        AnimationContainer.Create(() => AnimatedObject.CreateAt(owner.View),
                                  () => {
                                      if (owner.Team != target.Unit.Team)
                                          target.Unit.ApplyHPChange(owner, owner.Damage);
                                      if (!target.IsEmpty)
                                          target.Unit.Cage = to;
                                  },
                                  ao => {
                                      ao.SetSkillIconAsSprite("Hook")
                                        .SetSpeed(1400f)
                                        .MoveDefineTime(target.View).AfterThat()
                                        .ReleaseSequence()
                                        .MoveDefineTime(to.View)
                                        .MoveUnit(enemyView, to.View)
                                        .UpdateUnitStatus(enemyView)
                                        .AfterThat()
                                        .MoveDefineTime(ownerCageView)
                                        .Kill();
                                  }
        );
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