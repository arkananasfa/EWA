public class Raptor : Unit {
    public Raptor() : base(60, "Raptor", HPInfluence.NewDamage(16, DamageType.Physical, RangeType.Melee), 2, 0) {

        new FrontMover(this);
        new RaptorAttacker(this);

    }

}