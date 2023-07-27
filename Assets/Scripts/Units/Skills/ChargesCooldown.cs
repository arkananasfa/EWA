using System;

public class ChargesCooldown : Cooldown {

    public readonly int MaxCharges;
    public readonly int AddByTime;

    public int ChargesCount { get; set; }

    public override bool IsReady => ChargesCount > 0;

    /// <summary>
    /// Create cooldown with charges
    /// </summary>
    /// <param name="maxCharges">Limit of charges</param>
    /// <param name="full">Charges will adding one time in x moves</param>
    /// <param name="now">Start parameter of cooldown</param>
    /// <param name="startCharges">Charges in the start</param>
    /// <param name="addByTime">How many charges adding at once tick of cooldown</param>
    public ChargesCooldown(int maxCharges, int full, int now = 0, int startCharges = 0, int addByTime = 1) : base(full, now) {
        MaxCharges = maxCharges;
        AddByTime = addByTime == -1 ? maxCharges : addByTime;
        ChargesCount = startCharges;
    }

    public override void Refresh() {
        Now = Full;
        ChargesCount = MaxCharges;
        StateSet();
    }

    public override void Use() {
        ChargesCount--;
        StateSet();
    }

    public override void Decrease() {
        if (ChargesCount < MaxCharges) {
            Now--;
            if (Now <= 0) {
                ChargesCount++;
                Now = Full;
            }
        } else {
            Now = Full;
        }
        StateSet();
    }

}