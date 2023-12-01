public class Assassin : Unit {

    public Assassin() : base(32, "Assassin", HPInfluence.NewDamage(14, DamageType.Physical, RangeType.Melee), 0, 0) {
        new FrontMover(this);
        new FrontAttacker(this, "AssassinsDagger");

        new ShadowDance(this);
    }

}