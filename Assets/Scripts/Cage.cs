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
        if (Y - i > -1) return null;
        return Game.Map.GetCage(X, Y - i);
    }

    public Cage Left(int i = 1) {
        if (X - i > -1) return null;
        return Game.Map.GetCage(X - i, Y);
    }

    public Cage Right(int i = 1) {
        if (X + i < Game.Map.Width) return null;
        return Game.Map.GetCage(X + i, Y);
    }


    public Cage Down(int i = 1) {
        if (Y + i < Game.Map.Height) return null;
        return Game.Map.GetCage(X, Y + i);
    }

    public Cage Front(Team team, int i = 1) {
        return team.frontDirection == 1 ? Down(i) : Up(i);
    }

    public Cage Front(Unit unit, int i = 1) {
        return unit.Team.frontDirection == 1 ? Down(i) : Up(i);
    }

    public Cage Back(Team team, int i = 1) {
        return team.frontDirection == 1 ? Up(i) : Down(i);
    }

    public Cage Back(Unit unit, int i = 1) {
        return unit.Team.frontDirection == 1 ? Up(i) : Down(i);
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

}