using System.Collections.Generic;
using UnityEngine;

public class Swap : ActiveSkill {

    public Swap(Unit unit) : base(unit, "Swap", new Cooldown(3,1)) {
        applyEffect += Cast;
    }

    private void Cast(Cage cage) {
        Unit unit = cage.Unit;
        unit.Cage = owner.Cage;
        unit?.View.Move(owner.Cage.View, 0f, 0.1f);
        owner.Cage = cage;
        owner?.View.Move(cage.View, 0f, 0.1f);
    }

    protected override List<Cage> GetPossibleCages() {
        return CageListBuilder.New.UseAll().OnlyWithAllies(owner.Team).Remove(owner.Cage).Cages;
    }

}