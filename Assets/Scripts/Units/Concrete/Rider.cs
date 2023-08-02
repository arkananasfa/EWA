public class HorseRider : Unit {

    public HorseRider() : base(42, "Rider", HPInfluence.NewDamage(12, DamageType.Physical, RangeType.Melee), 2, 0) {
        new FrontMover(this, 2);
        new FrontAttacker(this);
    }

}