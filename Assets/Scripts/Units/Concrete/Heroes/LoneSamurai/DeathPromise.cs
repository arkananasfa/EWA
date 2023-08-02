using System.Collections.Generic;

public class DeathPromise : ActiveSkill {

    public DeathPromise(Unit unit) : base(unit, "DeathPromise", new Cooldown(1)) {
        applyEffect += CastDeathPromise;
    }

    private void CastDeathPromise(Cage cage) {
        Unit unit = cage.Unit;
        Effect effect = new Effect(unit, "DeathPromise", 5);
    }

    protected override List<Cage> GetPossibleCages() {
        return CageListBuilder.New.UseInRadius(owner.Cage, 3).OnlyWithEnemies(owner.Team).Cages;
    }

}