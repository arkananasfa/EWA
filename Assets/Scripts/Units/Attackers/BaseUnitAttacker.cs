public abstract class BaseUnitAttacker : UsableSkill {

    protected int distance;
    protected string attackProjectileCode;

    protected BaseUnitAttacker(Unit unit, string code, int distance, Cooldown cooldown, string attackVisual = "") : base(unit, code, cooldown, GameActionType.Attack) {
        applyEffect += Attack;
        this.distance = distance;
        attackProjectileCode = attackVisual;
        audioName = "Attack";
    }

    protected virtual void Attack(Cage attackCage) {
        UnitView unitView = attackCage.Unit.View;
        if (attackProjectileCode != "") {
            AnimationContainer.CreateProjectile(owner.Cage, attackCage, owner, attackCage.Unit, owner.Damage, attackProjectileCode);
        } else {
            unitView.UpdateStatus();
            attackCage.Unit.ApplyHPChange(owner, owner.Damage);
        }
    }

    protected virtual void Attack(Cage attackCage, float wait) {
        UnitView unitView = attackCage.Unit.View;
        if (attackProjectileCode != "") {
            AnimationContainer.CreateProjectileWait(owner.Cage, attackCage, owner, attackCage.Unit, owner.Damage, attackProjectileCode, wait);
        } else {
            unitView.UpdateStatus();
            attackCage.Unit.ApplyHPChange(owner, owner.Damage);
        }
    }

    public virtual int GetDistance() {
        return distance;
    }

    protected override void AddToUnit() {
        owner.SetAttacker(this);
    }

}