using System;
using System.Collections.Generic;

public class ActiveSkill : UsableSkill {
    
    /// <summary>
    /// Warning! PossibleTargets function must be added or GetPossibleCages overrided before using
    /// </summary>
    /// <param name="unit"></param>
    /// <param name="cooldown"></param>
    public ActiveSkill(Unit unit, string code, Cooldown cooldown) : base(unit, code, cooldown, GameActionType.Skill, 0) {
        SetNumber();
    }

    private void SetNumber() {
        skillNumber = owner.ActiveSkills.IndexOf(this);
    }

    protected override void AddToUnit() {
        owner.AddActiveSkill(this);
    }

}