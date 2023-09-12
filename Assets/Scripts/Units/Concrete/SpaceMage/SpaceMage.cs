public class SpaceMage : Unit {

    public SpaceMage() : base(34, "Space mage", HPInfluence.NewDamage(8, DamageType.Magical, RangeType.Ranged), 0, 2) {
        new FrontMover(this);
        new FrontAttacker(this, "SpaceMageRocket", 3);

        new Swap(this);
    }

}