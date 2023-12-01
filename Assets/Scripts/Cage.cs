using System;
using UnityEngine;
using Zenject;

public class Cage {

    public int X { get; private set; }
    public int Y { get; private set; }

    public bool IsEmpty => Unit == null;

    public CageView View { get; private set; }

    public Unit Unit {
        get { return _unit; }
        set {
            if (_unit != null) 
                View.UnitExit(_unit);
            _unit = value;
            if (_unit != null)
                View.UnitEnter(value);
        }
    }

    private Unit _unit;

    public Cage(int x, int y, CageView cageView) {
        X = x;
        Y = y;
        View = cageView;
        cageView.SetContext(this);
    }

    public Cage() { }

    public Cage Up(int i = 1) {
        if (Y - i < 0) return null;
        return Game.Map.GetCage(X, Y - i);
    }

    public Cage Left(int i = 1) {
        if (X - i < 0) return null;
        return Game.Map.GetCage(X - i, Y);
    }

    public Cage Right(int i = 1) {
        if (X + i >= Game.Map.Width) return null;
        return Game.Map.GetCage(X + i, Y);
    }


    public Cage Down(int i = 1) {
        if (Y + i >= Game.Map.Height) return null;
        return Game.Map.GetCage(X, Y + i);
    }

    public Cage Front(Team team, int i = 1) {
        return GetCageIn(0, team.frontDirection * i);
    }

    public Cage Front(Unit unit, int i = 1) {
        return GetCageIn(0, unit.Team.frontDirection * i);
    }

    public Cage Back(Team team, int i = 1) {
        return GetCageIn(0, -team.frontDirection * i);
    }

    public Cage Back(Unit unit, int i = 1) {
        return GetCageIn(0, -unit.Team.frontDirection * i);
    }

    public Cage GetCageIn(int x, int y) {
        int yy = y + Y;
        int xx = x + X;
        if (xx >= Game.Map.Width || xx < 0 || yy >= Game.Map.Height || yy < 0)
            return null;
        return Game.Map.GetCage(xx, yy);
    }

    public Cage GetCageIn(Team team, int right, int front) {
        int yy = team.frontDirection*front + Y;
        int xx = team.rightDirection*right + X;
        if (xx >= Game.Map.Width || xx < 0 || yy >= Game.Map.Height || yy < 0)
            return null;
        return Game.Map.GetCage(xx, yy);
    }

    public Cage GetCageIn(Unit unit, int right, int front) {
        int yy = unit.Team.frontDirection * front + Y;
        int xx = unit.Team.rightDirection * right + X;
        if (xx >= Game.Map.Width || xx < 0 || yy >= Game.Map.Height || yy < 0)
            return null;
        return Game.Map.GetCage(xx, yy);
    }

    public int Distance(Cage cage) {
        return XDistance(cage) + YDistance(cage);
    }

    public int XDistance(Cage cage) {
        return Math.Abs(cage.X - X);
    }

    public int YDistance(Cage cage) {
        return Math.Abs(cage.Y - Y);
    }

    public (int, int) Difference(Cage cage) {
        return (cage.X - X, cage.Y - Y); 
    }

    public int XDifference(Cage cage) {
        return cage.X - X;
    }

    public int YDifference(Cage cage) {
        return cage.Y - Y;
    }

    public bool IsInRadius(Cage other, int radius) {
        return Math.Max(XDistance(other), YDistance(other)) <= radius;
    }

    public override string ToString() {
        return $"[{X}; {Y}]";
    }

}