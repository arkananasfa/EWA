public class Hedgehogman : Unit {

    public Hedgehogman() : base(60, "Hedgehogman", HPInfluence.NewDamage(10, DamageType.Physical, RangeType.Melee), 4, 0) {

        new FrontMover(this);
        new FrontAttacker(this, "HNeedle");

        new BarbedArmor(this);

    }

}