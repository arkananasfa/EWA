public abstract class BaseUnitAttacker : UsableSkill {

    protected BaseUnitAttacker(Unit unit, string code, Cooldown cooldown) : base(unit, code, cooldown, GameActionType.Attack) {
        applyEffect += Attack;
    }

    public virtual void Attack(Cage attackCage) {
        attackCage.Unit.ApplyHPChange(owner, owner.Damage);

    }

}