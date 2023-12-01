using System;
using System.Collections.Generic;
using UnityEngine;

public class FishmanAttacker : FrontAttacker {

    public FishmanAttacker(Unit unit) : base(unit, "Spear", 3) {}

    protected override void Attack(Cage attackCage) {
        List<UnitView> unitViews = new();
        List<CageView> cageViews = new();
        var startView = owner.Cage.View;
        AnimationContainer.Create(
            () => AnimatedObject.CreateProjectileAt(startView, attackProjectileCode).SetSpeed(1000),
            () => {
                for (int i = 0; i < 3; i++) {
                    Cage cage = owner.Cage.GetCageIn(0, (i+1) * owner.Team.frontDirection);
                    if (cage == null)
                        break;
                    if (cage.Unit != null && !cage.Unit.IsTeammate(owner)) {
                        Debug.Log(cage.Unit.Name);
                        unitViews.Add(cage.Unit.View);
                        cageViews.Add(cage.View);
                        cage.Unit.ApplyHPChange(owner, HPInfluence.NewDamage(owner.Damage.Value / 3m * (3-i), DamageType.Physical, RangeType.Ranged));
                    } else {
                        unitViews.Add(null);
                        cageViews.Add(cage.View);
                    }
                }
            },
            ao => {
                for (int i = 0;i<unitViews.Count;i++) {
                    ao.MoveDefineTime(cageViews[i]).AfterThat();
                    unitViews[i]?.UpdateStatus();
                }
                ao.Kill();
            }
        );
        
    }

}