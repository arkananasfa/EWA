public class RangedImp : Unit {

    public RangedImp() : base(16, "RangedImp", HPInfluence.NewDamage(10, DamageType.Physical, RangeType.Ranged), 1, 1) {
        new FrontMover(this);
        new FrontAttacker(this, "Arrow");
    }

}