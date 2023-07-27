public class HellHound : Unit {

    public HellHound() : base(50, HPInfluence.NewDamage(16, DamageType.Physical, RangeType.Melee), 2, 1) {
        SetMover(new FrontMover(this));
        SetAttacker(new FrontAttacker(this));
    }

}