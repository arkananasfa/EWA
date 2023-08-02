using UnityEngine;

public abstract class BaseUnitAttacker : UsableSkill {

    protected int distance;

    protected BaseUnitAttacker(Unit unit, string code, int distance, Cooldown cooldown) : base(unit, code, cooldown, GameActionType.Attack) {
        applyEffect += Attack;
        this.distance = distance;
    }

    protected virtual void Attack(Cage attackCage) {
        attackCage.Unit.ApplyHPChange(owner, owner.Damage);
    }

    public virtual int GetDistance() {
        return distance;
    }

    protected override void AddToUnit() {
        owner.SetAttacker(this);
    }

}