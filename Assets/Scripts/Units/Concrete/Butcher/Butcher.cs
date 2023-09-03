public class Butcher : Unit {

    public Butcher() : base(85, "Butcher", HPInfluence.NewDamage(20, DamageType.Physical, RangeType.Melee), 4, 0) {
        
        new FrontMover(this);
        new FrontAttacker(this, "Hook");

        new Hook(this);
        
    }

}