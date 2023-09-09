using System.Collections.Generic;
using System.Linq;

public class BarbedArmor : Skill {

    public static readonly int castLimit = 10;
    private decimal _damageRequired = castLimit;

    public BarbedArmor(Unit unit) : base(unit, "BarbedArmor", Cooldown.NoCooldown) {
        OnHpInfluenceApplied += TryCastBarbedArmor;
    }

    private void TryCastBarbedArmor(Unit attacker, Unit defender, HPInfluence hpInfluence) {
        if (hpInfluence.Type == HPChangeType.Damage && defender == owner) {
            _damageRequired -= hpInfluence.Value;
            while (_damageRequired <= 0) {
                _damageRequired += castLimit;
                CageView ownerCageView = owner.Cage.View;
                List<Cage> cagesNear = CageListBuilder.New.Use8Neighbor(owner.Cage).Cages;
                var unitsNear = CageListBuilder.New.Use8Neighbor(owner.Cage).Cages.Where(c => c.Unit != null && c.Unit.Team != owner.Team).Select(c => c.Unit).ToList();
                var unitViewsNear = unitsNear.Select(u => u.View).ToList();
                AnimationContainer.Create(() => AnimatedObject.CreateProjectileAt(ownerCageView, "HNeedles"),
                                          () => {
                                              unitsNear.ForEach(u => u.ApplyHPChange(owner, owner.Damage));
                                          },
                                          ao => {
                                              ao.WaitFor(1f / 10f)
                                                .AfterThat().ReleaseSequence();
                                              unitViewsNear.ForEach(uv => ao.UpdateUnitStatus(uv));
                                              ao.Kill();
                                          }
                );
            }
        }
    }

}