using System.Collections.Generic;

public class ShadowDance : ActiveSkill {

    public ShadowDance(Unit unit) : base(unit, "ShadowDance", new Cooldown(2, 1)) {
        applyEffect += Cast;
    }

    private void Cast(Cage target) {
        var unit = target.Unit;
        var unitView = unit.View;
        var ownerView = owner.View;
        var cageToGo = target.Front(owner.Team);
        owner.Cage = cageToGo;
        AnimationContainer.Create(() => AnimatedObject.CreateProjectileAt(cageToGo.View, "AssassinsDagger"),
                                  () => {
                                      unit.ApplyHPChange(owner, HPInfluence.NewDamage(owner.Damage.Value*1.5m, DamageType.Physical, RangeType.Melee));
                                      unitView.UpdateStatus();
                                      decimal poisonDamage = owner.Damage.Value / 2m;
                                      Effect effect = new Effect(unit, "Poisoned", 2, Effect.Power.Weak, Effect.Purpose.Bad).WithVisual();
                                      effect.OnMoveStarted += () => {
                                          unit.ApplyHPChange(unit, HPInfluence.NewDamage(poisonDamage, DamageType.Physical, RangeType.Melee));
                                          if (unitView != null) {
                                              unitView.UpdateStatus();
                                          }
                                          effect.UseVisual();
                                      };
                                  },
                                  ao => {
                                      ao.HideFor(0.2f)
                                        .InSeconds(0.2f)
                                        .MoveUnit(ownerView, cageToGo.View)
                                        .AfterThat()
                                        .MoveDefineTime(target.View).AfterThat()
                                        .ReleaseSequence()
                                        .Kill();
                                  }
        );
    }

    protected override List<Cage> GetPossibleCages() {
        return CageListBuilder.New.UseInRadius(owner.Cage, 2).OnlyWithEnemies(owner.Team).OnlyIf(c => c.Front(owner.Team) != null && c.Front(owner.Team).IsEmpty).Cages;
    }

}