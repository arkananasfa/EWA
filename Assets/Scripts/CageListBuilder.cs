using System;
using System.Collections.Generic;
using System.Linq;

public class CageListBuilder {

    public List<Cage> Cages => _cages.ToList();

    private HashSet<Cage> _cages;

    public CageListBuilder() { 
        _cages = new();
    }

    public static CageListBuilder New => new CageListBuilder();

    public CageListBuilder UseAll() {
        for (int i = 0;i<Game.Map.Height;i++) {
            UseRow(i);
        }
        return this;
    }

    public CageListBuilder UseCage(Cage cage) {
        _cages.Add(cage);
        return this;
    }

    public CageListBuilder UseRow(int row) {
        var cages = Game.Map.GetRow(row);
        foreach (var cage in cages) {
            _cages.Add(cage);
        }
        return this;
    }

    public CageListBuilder Use8Neighbor(Cage cage) {
        return UseInRadius(cage, 1);
    }

    public CageListBuilder Use4Neighbor(Cage cage) {
        Cage c = cage.Left();
        if (c != null) _cages.Add(c);
        c = cage.Right();
        if (c != null) _cages.Add(c);
        c = cage.Up();
        if (c != null) _cages.Add(c);
        c = cage.Down();
        if (c != null) _cages.Add(c);
        return this;
    }

    public CageListBuilder UseInRadius(Cage cage, int r) {
        int startY = cage.Y - r;
        int startX = cage.X - r;
        for (int y = startY; y <= startY + r*2; y++) {
            for (int x = startX; x <= startX + r*2; x++) {
                if (x == cage.X && y == cage.Y)
                    continue;
                Cage c = Game.Map.GetCage(x, y);
                if (c != null)
                    _cages.Add(c);
            }
        }
        return this;
    }

    public CageListBuilder UseTeamHome(Team team) {
        UseRow(team.HomeY);
        return this;
    }

    public CageListBuilder OnlyEmpty() {
        _cages = _cages.Where(c => c.IsEmpty).ToHashSet();
        return this;
    }

    public CageListBuilder OnlyWithUnit() {
        _cages = _cages.Where(c => !c.IsEmpty).ToHashSet();
        return this;
    }

    public CageListBuilder OnlyWithEnemies(Team team) {
        OnlyWithUnit();
        _cages = _cages.Where(c => c.Unit.Team != team).ToHashSet();
        return this;
    }

    public CageListBuilder OnlyWithAllies(Team team) {
        OnlyWithUnit();
        _cages = _cages.Where(c => c.Unit.Team == team).ToHashSet();
        return this;
    }

    public CageListBuilder OnlyIf(Func<Cage, bool> condition) {
        _cages = _cages.Where(condition).ToHashSet();
        return this;
    }

    public CageListBuilder Remove(Cage cage) {
        _cages.Remove(cage);
        return this;
    }

    public CageListBuilder Remove(List<Cage> cages) {
        foreach (Cage cage in cages) {
            _cages.Remove(cage);
        }
        return this;
    }

    public CageListBuilder Add(List<Cage> cages) {
        foreach (Cage cage in cages) {
            _cages.Add(cage);
        }
        return this;
    }

    public CageListBuilder Remove(CageListBuilder cageListBuilder) {
        foreach (Cage cage in cageListBuilder._cages) {
            _cages.Remove(cage);
        }
        return this;
    }
    
    public CageListBuilder Add(CageListBuilder cageListBuilder) {
        foreach (Cage cage in cageListBuilder._cages) {
            _cages.Add(cage);
        }
        return this;
    }

}