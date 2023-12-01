public class Fairy : Unit {

    public Fairy() : base(42, "Fairy", HPInfluence.NewDamage(5, DamageType.Magical, RangeType.Ranged), 0, 4) {
        new FrontMover(this);
        new FairyAttacker(this);

        new Skill(this, "DustBlast", Cooldown.NoCooldown);
    }

}