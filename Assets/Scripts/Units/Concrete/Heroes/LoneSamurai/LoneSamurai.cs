public class LoneSamurai : Hero {

    public LoneSamurai() : base(87, "Lone samurai", HPInfluence.NewDamage(24, DamageType.Physical, RangeType.Melee), 5, 0) {

        new HeroMover(this, 2);
        new HeroAttacker(this, 1);

        new TsunamiStrike(this);
        
        new LightningBlink(this);
        new DeathPromise(this);
        new ElderSamuraiForm(this);

    }

}