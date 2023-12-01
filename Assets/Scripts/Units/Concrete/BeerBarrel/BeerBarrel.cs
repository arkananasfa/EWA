public class BeerBarrel : Unit {

    public BeerBarrel() : base(30, "Beer barrel", HPInfluence.NewDamage(0, DamageType.Physical, 0), 2, 0) {
        new FrontMover(this);
        new FrontAttacker(this, "");

        new FunAtmosphere(this);
    }

}