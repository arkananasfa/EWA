using System.Collections.Generic;
using System.Linq;

public class CageListBuilder {

    public List<Cage> Cages;

    public CageListBuilder() { 
        Cages = new();
    }

    public static CageListBuilder New() {
        return new CageListBuilder();
    }

    public CageListBuilder UseRow(int row) {
        Cages.AddRange(Game.Map.GetRow(row));
        return this;
    }

    public CageListBuilder Use8Neighbor(Cage cage) {
        int startY = cage.Y - 1;
        int startX = cage.X - 1;
        for (int y = startY;y<startY+2;y++) {
            for (int x = startX;x<startX+2;x++) {
                if (x == cage.X && y == cage.Y) 
                    continue;
                Cage c = Game.Map.GetCage(x, y);
                if (c != null)
                    Cages.Add(c);
            }
        }
        return this;
    }

    public CageListBuilder Use4Neighbor(Cage cage) {
        Cage c = cage.Left();
        if (c != null) Cages.Add(c);
        c = cage.Right();
        if (c != null) Cages.Add(c);
        c = cage.Up();
        if (c != null) Cages.Add(c);
        c = cage.Down();
        if (c != null) Cages.Add(c);
        return this;
    }

    public CageListBuilder OnlyEmpty() {
        Cages = Cages.Where(c => c.IsEmpty).ToList();
        return this;
    }

}