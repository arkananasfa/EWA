public class Invoker : Unit {

    public Invoker() : base(36, "Invoker", HPInfluence.NewDamage(12, DamageType.Magical, RangeType.Ranged), 0, 2) {
        new FrontMover(this);
        new FrontAttacker(this, "Arrow", 3);

        new SummonMeleeImp(this);
        new SummonRangedImp(this);
        new SummonMageImp(this);
    }

}