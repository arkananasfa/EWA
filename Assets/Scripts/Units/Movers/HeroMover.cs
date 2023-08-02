using System.Collections.Generic;
using UnityEngine;

public class HeroMover : BaseUnitMover {

    protected int distance;

    public HeroMover(Unit unit, int distance, string code = "HeroMover") : base(unit, code, new Cooldown(1, 0), false) {
        this.distance = distance;
    }

    public override int GetDistance() {
        return distance;
    }

    protected override List<Cage> GetPossibleCages() {
        var mainBuilder = CageListBuilder.New.UseCage(owner.Cage);
        var lastCageBuilder = CageListBuilder.New.UseCage(owner.Cage);
        for (int i = 0; i < distance; i++) {
            var tmpCageBuilder = CageListBuilder.New;
            foreach (var cage in lastCageBuilder.Cages) {
                tmpCageBuilder.Use8Neighbor(cage).OnlyEmpty();
            }
            tmpCageBuilder.Remove(mainBuilder);
            mainBuilder.Add(tmpCageBuilder);
            lastCageBuilder = tmpCageBuilder;
        }
        return mainBuilder.Remove(new List<Cage> { owner.Cage }).Remove(CageListBuilder.New.UseTeamHome(owner.Team.Opponent)).Cages;
    }

}