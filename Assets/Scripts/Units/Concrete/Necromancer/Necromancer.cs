public class Necromancer : Unit {

    public Necromancer() : base(52, "Necromancer", HPInfluence.NewDamage(16, DamageType.Magical, RangeType.Ranged), 0, 2) {

        new FrontMover(this);
        new FrontAttacker(this, "NecromancerProjectile", 2);

        new Necromancy(this);

    }

}