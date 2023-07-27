using UnityEngine;

public class UnitsFactory {

    public virtual Unit CreateUnit(UnitType unitType) {
        Unit unit = unitType switch {
            UnitType.Swordsman => new Swordsman(),
            UnitType.Archer => new Archer(),
            UnitType.HellHound => new HellHound(),
            _ => new HellHound()
        };
        return unit;
    }

}