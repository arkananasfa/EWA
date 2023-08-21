using System.Collections.Generic;

public abstract class SummonSkill : ActiveSkill {

    protected abstract UnitType UnitType { get; set; }

    public SummonSkill(Unit unit, string code, Cooldown cooldown) : base(unit, code, cooldown) {

        applyEffect += Summon;
    }

    protected virtual void Summon(Cage cage) {
        Unit newUnit = Game.UnitsFactory.CreateUnit(UnitType);
        newUnit.Cage = cage;
        var archiveElement = Game.UnitsArchive.GetElementByUnitType(UnitType);
        newUnit.Init(archiveElement.sprite, cage, Game.CurrentPlayer);
    }

}