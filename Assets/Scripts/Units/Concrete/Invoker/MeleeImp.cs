public class MeleeImp : Unit {

    public MeleeImp() : base(24, "MeleeImp", HPInfluence.NewDamage(8, DamageType.Physical, RangeType.Melee), 2, 0) {
        new FrontMover(this);
        new FrontAttacker(this, "Sword");
    }

}