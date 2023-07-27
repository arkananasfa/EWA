public abstract class BaseUnitMover : UsableSkill {

    protected BaseUnitMover(Unit unit, string code, Cooldown cooldown) : base(unit, code, cooldown, GameActionType.Move) {
        applyEffect += Move;
    }

    public virtual void Move(Cage target) {
        owner.Cage = target;
    }

}