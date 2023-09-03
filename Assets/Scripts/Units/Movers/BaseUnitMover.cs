public abstract class BaseUnitMover : UsableSkill {

    public bool IsMustMove;

    protected BaseUnitMover(Unit unit, string code, Cooldown cooldown, bool isMustMove = true) : base(unit, code, cooldown, GameActionType.Move) {
        applyEffect += Move;
        IsMustMove = isMustMove;
        audioName = "Move";
    }

    protected virtual void Move(Cage target) {
        owner.Cage = target;
        owner.View.Move(target);
        owner.View.UpdateStatus();
    }

    public abstract int GetDistance();

    protected override void AddToUnit() {
        owner.SetMover(this);
    }

}