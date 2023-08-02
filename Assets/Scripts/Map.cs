using System.Collections.Generic;
using UnityEngine;

public class Map {

    public int Width { get; set; }
    public int Height { get; set; }

    private Cage[,] _cages;

    public Map(int w, int h, Transform cagesParent, CageView cagePrefab) {
        Width = w;
        Height = h;
        _cages = new Cage[w, h];

        for (int y = 0; y < h; y++) {
            for (int x = 0; x < w; x++) {
                Cage cage = new Cage(x, y, Object.Instantiate(cagePrefab, cagesParent));
                _cages[x, y] = cage;
            }
        }
    }

    public Cage GetCage(int x, int y) {
        if (x < Game.Map.Width && x > -1 && y < Game.Map.Height && y > -1)
            return _cages[x, y];
        return null;
    }

    public CageView GetCageView (int x, int y) {
        return GetCage(x, y).View;
    }

    public List<Cage> GetRow(int y) {
        List<Cage> row = new(Width);
        for (int x = 0;x < Width;x++) {
            row.Add(_cages[x, y]);
        }
        return row;
    }

}