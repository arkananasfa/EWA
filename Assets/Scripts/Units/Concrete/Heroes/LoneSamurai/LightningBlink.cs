using System;
using System.Collections.Generic;

public class LightningBlink : ActiveSkill {

    private List<Unit> _attackedUnits;

    public LightningBlink(Unit unit) : base(unit, "LightningBlink", new ChargesCooldown(3, 1, 1, 3, 1)) {
        _attackedUnits = new(3);

        applyEffect += CastLightningBlink;
        OnMoveEnded += ClearAttackedEnemies;
    }

    private void CastLightningBlink(Cage cage) {
        var yDiff = owner.Cage.YDifference(cage)/2;
        var xDiff = owner.Cage.XDifference(cage)/2;
        var isXDiff = xDiff != 0;
        var unit = isXDiff ? owner.Cage.GetCageIn(xDiff, 0).Unit : owner.Cage.GetCageIn(0, yDiff).Unit;
        unit.ApplyHPChange(owner, owner.Damage);
        _attackedUnits.Add(unit);
        owner.Mover.Apply(cage);
    }

    private void ClearAttackedEnemies() {
        _attackedUnits.Clear();
    }

    protected override List<Cage> GetPossibleCages() {
        CageListBuilder enemiesCanBeAttacked = CageListBuilder.New.Use4Neighbor(owner.Cage).OnlyWithEnemies(owner.Team).OnlyIf(c => !_attackedUnits.Contains(c.Unit));
        CageListBuilder mainBuilder = CageListBuilder.New;
        foreach (Cage cage in enemiesCanBeAttacked.Cages)
            mainBuilder.Add(CageListBuilder.New.Use4Neighbor(cage).OnlyEmpty());
        mainBuilder.Remove(CageListBuilder.New.Use8Neighbor(owner.Cage)).Remove(CageListBuilder.New.UseTeamHome(owner.Team.Opponent));
        return mainBuilder.Cages;
    }

}