public class Sceleton : Unit {

    public Sceleton() : base(30, "Sceleton", HPInfluence.NewDamage(12, DamageType.Physical, RangeType.Melee), 2, 0) {

        new FrontMover(this);
        new FrontAttacker(this, "Sword");

    }

}