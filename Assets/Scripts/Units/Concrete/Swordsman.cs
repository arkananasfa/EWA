public class Swordsman : Unit {

    public Swordsman() : base(40, HPInfluence.NewDamage(10, DamageType.Physical, RangeType.Melee), 5, 0) {
        SetMover(new FrontMover(this));
        SetAttacker(new FrontAttacker(this));
    }

}