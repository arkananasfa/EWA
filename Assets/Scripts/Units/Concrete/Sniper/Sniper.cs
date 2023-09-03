public class Sniper : Unit {

    public Sniper() : base(24, "Sniper", HPInfluence.NewDamage(24, DamageType.Physical, RangeType.Ranged), 0, 0) {
        
        new FrontMover(this);
        new FrontAttacker(this, "Bullet", 7);

        new SniperShot(this);
        
    }

}