public class HolyPrincess : Hero {

    public HolyPrincess() : base(70, "HolyPrincess", HPInfluence.NewDamage(17, DamageType.Magical, RangeType.Ranged), 2, 6) {

        new HeroMover(this, 1);
        new HeroAttacker(this, "PrincessProjectile", 2);

        new HolyShield(this);

        new HolyBlessing(this);
        new HolyPunishment(this);
        new HolyRequiem(this);

    }

}