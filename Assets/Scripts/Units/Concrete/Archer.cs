public class Archer : Unit {

    public Archer() : base(32, "Archer", HPInfluence.NewDamage(12, DamageType.Physical, RangeType.Ranged), 1, 0) {
        new FrontMover(this);
        new FrontAttacker(this, 3);
    }

}