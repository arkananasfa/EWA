using System.Collections.Generic;
using UnityEngine;

public class Team {

    public readonly int frontDirection;
    public readonly int rightDirection;

    public Color Color { get; set; }

    public IEnumerable<Unit> Units => _units;
    public Team Opponent => this == Game.Team1 ? Game.Team2 : Game.Team1;

    private List<Unit> _units;

    public Team(int frontDirection) { 
        this.frontDirection = frontDirection;
        rightDirection = -frontDirection;
    }

    public void AddUnit(Unit unit) {
        _units.Add(unit);
    }

    public void RemoveUnit(Unit unit) {
        _units.Remove(unit);
    }

}