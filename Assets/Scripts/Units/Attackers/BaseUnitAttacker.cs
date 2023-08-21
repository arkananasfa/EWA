using UnityEngine;

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
        attackCage.Unit.ApplyHPChange(owner, owner.Damage);
        if (attackProjectileCode != "") {
            var go = Object.Instantiate(Game.SpritesExtractor.GetAttackVisual(attackProjectileCode), owner.Cage.View.transform);
            var attackVisual = go.GetComponent<AttackVisual>();
            attackVisual.FlyTo(attackCage.View);
        }
    }

    public virtual int GetDistance() {
        return distance;
    }

    protected override void AddToUnit() {
        owner.SetAttacker(this);
    }

}