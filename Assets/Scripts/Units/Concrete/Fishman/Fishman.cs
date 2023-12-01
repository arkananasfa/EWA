public class Fishman : Unit {

    public Fishman() : base(54, "Fishman", HPInfluence.NewDamage(18, DamageType.Physical, RangeType.Ranged), 2, 0) {
        new FrontMover(this);
        new FishmanAttacker(this);

        new Skill(this, "LongStrike", Cooldown.NoCooldown);
    }

}