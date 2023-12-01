public class Whale : Unit {

    public Whale() : base(33, "Whale", HPInfluence.NewDamage(10, DamageType.Magical, RangeType.Ranged), 0, 0) {
        new FrontMover(this);
        new WhaleAttacker(this);

        new Skill(this, "Wave", new Cooldown(3, 1));
    }

}