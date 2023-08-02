using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Team {

    public readonly int frontDirection;
    public readonly int rightDirection;

    public Color Color { get; set; }
    public int HomeY { get; set; }

    public IEnumerable<Unit> Units => _units;
    public Team Opponent => this == Game.Team1 ? Game.Team2 : Game.Team1;

    private List<Unit> _units;

    public Team(int frontDirection) { 
        this.frontDirection = frontDirection;
        rightDirection = -frontDirection;
        HomeY = frontDirection == -1 ? Game.Map.Height - 1 : 0;
        _units = new List<Unit>();
    }

    public void AddUnit(Unit unit) {
        _units.Add(unit);
    }

    public void RemoveUnit(Unit unit) {
        _units.Remove(unit);
    }

    public void ApplyEndMoveMoving() {
        var sortedUnits = from unit in _units
                          orderby -frontDirection * unit.Cage.Y
                          select unit;
        sortedUnits.ToList().ForEach(unit => {
            if (unit.Mover.IsMustMove)
                while (unit.Mover.CanUse())
                    unit.Mover.Use();
        });
    }

}