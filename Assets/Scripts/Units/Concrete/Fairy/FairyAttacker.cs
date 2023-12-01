using System.Collections.Generic;

public class FairyAttacker : BaseUnitAttacker {

    public FairyAttacker(Unit unit) : base(unit, "HeroAttacker", 2, new Cooldown(1, 1), "FairyDust") {}

    protected override void Attack(Cage attackCage) {
        float wait = 0;
        List<Cage> enemiesCages = CageListBuilder.New.UseInRadius(owner.Cage, 2).OnlyWithEnemies(owner.Team).Cages;
        foreach (var cage in enemiesCages) {
            var unit = cage.Unit;
            var unitView = unit.View;
            AnimationContainer.CreateProjectileWait(owner.Cage, cage, owner, unit, owner.Damage, attackProjectileCode, wait);
            wait += 0.08f;
        }
    }

    protected override List<Cage> GetPossibleCages() {
        return new List<Cage> { owner.Cage };
    }

}