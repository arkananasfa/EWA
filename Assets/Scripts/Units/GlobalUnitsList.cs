using System.Collections.Generic;

public class GlobalUnitList {

    public List<Unit> Units { get; set; }

    public GlobalUnitList()
    {
        Units = new List<Unit>();
    }

    public void Add(Unit unit) {
        Units.Add(unit);
        unit.Team.AddUnit(unit);
    }

}