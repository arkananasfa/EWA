public class Berserk : Unit {

    public Berserk() : base(60, "Berserk", HPInfluence.NewDamage(7, DamageType.Physical, RangeType.Ranged), 0, 0) {
        new FrontMover(this);
        new BerserkAttacker(this);

        new Skill(this, "Rage", Cooldown.NoCooldown);
    }

    public override void AfterInit() {
        SetParameter((Attacker as BerserkAttacker).AttackCount);
    }

}