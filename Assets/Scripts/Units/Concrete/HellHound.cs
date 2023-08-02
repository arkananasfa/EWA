public class HellHound : Unit {

    public HellHound() : base(50, "Hell hound", HPInfluence.NewDamage(16, DamageType.Physical, RangeType.Melee), 2, 1) {
        new FrontMover(this);
        new HellHoundAttacker(this);
    }

}