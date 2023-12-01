using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WhaleAttacker : BaseUnitAttacker {

    public WhaleAttacker(Unit unit) : base(unit, "FrontAttacker", 7, new Cooldown(3, 1), "Wave") {}

    protected override void Attack(Cage attackCage) {
        List<UnitView> unitViews = new();
        List<CageView> cageViews = new();
        List<bool> isPushing = new();
        var startView = owner.Cage.View;
        AnimationContainer.Create(
            () => AnimatedObject.CreateProjectileAt(startView, attackProjectileCode).SetSpeed(1000),
            () => {
                for (int i = 0; i < 7; i++) {
                    Cage cage = owner.Cage.GetCageIn(0, (i + 1) * owner.Team.frontDirection);
                    if (cage == null)
                        break;
                    if (cage.Unit != null && !cage.Unit.IsTeammate(owner)) {
                        unitViews.Add(cage.Unit.View);
                        cage.Unit.ApplyHPChange(owner, owner.Damage);
                    } else if (cage.Unit != null) {
                        unitViews.Add(cage.Unit.View);
                    } else { 
                        unitViews.Add(null);
                    }
                    cageViews.Add(cage.View);
                }
                for (int i = 7; i > 0; i--) {
                    Cage cage = owner.Cage.GetCageIn(0, i * owner.Team.frontDirection);
                    if (cage == null)
                        continue;
                    if (cage.Unit != null) {
                        if (cage.Front(owner.Team) != null && cage.Front(owner.Team).IsEmpty) {
                            isPushing.Insert(0, true);
                            cage.Unit.Cage = cage.Front(owner.Team);
                        } else if (isPushing.Count > 0 && isPushing[0]) {
                            isPushing.Insert(0, true);
                            cage.Unit.Cage = cage.Front(owner.Team);
                        } else {
                            isPushing.Insert(0, false);
                        }
                    } else {
                        isPushing.Insert(0, false);
                    }
                }
            },
            ao => {
                foreach (var a in isPushing) {
                    Debug.Log(a);
                }
                for (int i = 0; i < unitViews.Count; i++) {
                    ao.MoveDefineTime(cageViews[i]);
                    unitViews[i]?.UpdateStatus();
                    if (isPushing[i]) {
                        unitViews[i].Move(owner.Cage.Front(owner.Team, i + 1));
                    }
                    ao.AfterThat();
                }
                ao.Kill();
            }
        );

    }

    protected override List<Cage> GetPossibleCages() {
        return new List<Cage> { owner.Cage };
    }

}