using System;
using System.Collections.Generic;

public class FrontMover : BaseUnitMover {

    public FrontMover(Unit unit, int chargescount = 1) : base(unit, "FrontMover", new ChargesCooldown(chargescount, 1, 1, 0, chargescount)) {
    }

    protected override List<Cage> GetPossibleCages() {
        Cage frontCage = owner.Cage.Front(owner, 1);
        if (frontCage == null) return new List<Cage>();
        if (!frontCage.IsEmpty) return new List<Cage>();
        return new() { frontCage };
    }

}