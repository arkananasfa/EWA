public class Swordsman : Unit {

    public Swordsman() : base(40, "Swordsman", HPInfluence.NewDamage(10, DamageType.Physical, RangeType.Melee), 5, 0) {
        new FrontMover(this);
        new FrontAttacker(this);
    }

}