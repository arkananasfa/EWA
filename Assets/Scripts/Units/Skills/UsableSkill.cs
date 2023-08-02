using System;
using System.Collections.Generic;

public abstract class UsableSkill : Skill {

    public event Action OnUsed;

    protected Func<List<Cage>> getPossibleTargets;
    protected Action<Cage> applyEffect;
    
    protected GameActionType actionType;
    protected int skillNumber;

    public UsableSkill(Unit unit, string code, Cooldown cooldown, GameActionType type, int skillNumber = 0) : base(unit, code, cooldown) {
        actionType = type;
        this.skillNumber = skillNumber;
    }

    public UsableSkill AddGetPossibleCagesFunction(Func<List<Cage>> getPossibleCagesFunction) {
        getPossibleTargets = getPossibleCagesFunction;
        return this;
    }

    public UsableSkill AddApplyEffect(Action<Cage> applyEffect) {
        this.applyEffect = applyEffect;
        return this;
    }

    public virtual void Apply(Cage target) {
        if (applyEffect != null) 
            applyEffect(target);
        Cooldown.Use();
    }

    public virtual void Cancel() { }

    public virtual void Use() {
        if (!CanUse()) return;
        Game.CageChooseManager.SetAction(CreateAction());
        OnUsed?.Invoke();
    }

    public override bool CanUse() {
        return base.CanUse() && GetPossibleCages().Count>0;
    }

    protected virtual GameAction CreateAction() {
        var action = new GameAction();
        action.Type = actionType;
        action.Parameter = skillNumber;
        action.Unit = owner;
        action.PossibleTargets = GetPossibleCages();
        return action;
    }

    protected virtual List<Cage> GetPossibleCages() {
        return getPossibleTargets();
    }

}