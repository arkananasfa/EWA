using System;
using System.Collections.Generic;

public class ActiveSkill : UsableSkill {
    
    public ActiveSkill(Unit unit, string code, Cooldown cooldown, Func<List<Cage>> targetsFindFunction) : base(unit, code, cooldown, GameActionType.Skill, 0) {
        getPossibleTargets = targetsFindFunction;
        owner.AddActiveSkill(this);
        SetNumber();
    }

    /// <summary>
    /// Warning! PossibleTargets function must be added or GetPossibleCages overrided before using
    /// </summary>
    /// <param name="unit"></param>
    /// <param name="cooldown"></param>
    public ActiveSkill(Unit unit, string code, Cooldown cooldown) : base(unit, code, cooldown, GameActionType.Skill, 0) {
        owner.AddActiveSkill(this);
        SetNumber();
    }

    private void SetNumber() {
        skillNumber = owner.ActiveSkills.IndexOf(this);
    }

}