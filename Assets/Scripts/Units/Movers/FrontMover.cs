using System.Collections.Generic;

public class FrontMover : BaseUnitMover {

    public FrontMover(Unit unit, int chargescount = 1) : base(unit, "FrontMover", new ChargesCooldown(chargescount, 1, 1, 0, chargescount)) {
    }

    public override int GetDistance() {
        return (Cooldown as ChargesCooldown).MaxCharges;
    }

    protected override List<Cage> GetPossibleCages() {
        Cage frontCage = owner.Cage.Front(owner);
        if (frontCage == null) return new List<Cage>();
        if (!frontCage.IsEmpty) return new List<Cage>();
        return new() { frontCage };
    }

}