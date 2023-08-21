public class HellHoundAttacker : FrontAttacker {

    public HellHoundAttacker(Unit unit, int distance = 1, int times = 1) : base(unit, "HellHoundAttacker", "HellHoundAttackProjectile", distance, times) {
    }

    protected override void Attack(Cage attackCage) {
        base.Attack(attackCage);
        owner.Damage.Value += 3;
    }

}