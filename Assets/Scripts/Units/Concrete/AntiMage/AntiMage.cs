public class AntiMage : Unit {

    public AntiMage() : base(40, "Anti mage", HPInfluence.NewDamage(12, DamageType.Magical, RangeType.Melee), 0, 6) {

        new FrontMover(this);
        new FrontAttacker(this, "AntiMageSword");

        new MagicWeakening(this);

    }

}