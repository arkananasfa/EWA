using System.Collections.Generic;

public class RaptorAttacker : BaseUnitAttacker {

    public RaptorAttacker(Unit unit) : base(unit, "RaptorAttacker", 1, new Cooldown(1, 1), "RaptorPaws") {
    }

    protected override void Attack(Cage attackCage) {
        List<Cage> enemiesCagesNear = CageListBuilder.New.Use4Neighbor(attackCage).OnlyWithEnemies(owner.Team).Cages;
        foreach (Cage c in enemiesCagesNear) {
            AnimationSequence.New().AddMain(AnimatedObject.CreateAttackProjectile(owner.Cage, attackCage, "RaptorPaws"));
            attackCage.Unit.ApplyHPChange(owner, owner.Damage);
        }
        UnitView view = owner.View;
        owner.Cage = attackCage;
        view.Move(owner.Cage);
        view.UpdateStatus();
    }

    protected override List<Cage> GetPossibleCages() {
        List<Cage> cages = new();

        Cage cage = owner.Cage.GetCageIn(-1, owner.Team.frontDirection);
        if (cage != null && cage.IsEmpty) 
            cages.Add(cage);

        cage = owner.Cage.GetCageIn(1, owner.Team.frontDirection);
        if (cage != null && cage.IsEmpty)
            cages.Add(cage);

        return cages;
    }

}