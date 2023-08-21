public class MageImp : Unit {

    public MageImp() : base(10, "MageImp", HPInfluence.NewDamage(12, DamageType.Magical, RangeType.Ranged), 0, 2) {
        new FrontMover(this);
        new FrontAttacker(this, "PrincessProjectile");
    }

}