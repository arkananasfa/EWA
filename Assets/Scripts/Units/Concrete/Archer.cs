public class Archer : Unit {

    public Archer() : base(32, HPInfluence.NewDamage(12, DamageType.Physical, RangeType.Ranged), 1, 0) {
        SetMover(new FrontMover(this));
        SetAttacker(new FrontAttacker(this));
    }

}