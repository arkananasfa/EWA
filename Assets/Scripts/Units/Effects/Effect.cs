using System;

public class Effect : Skill {

    public int Duration { get; set; }

    private Action onEffectEnded;

    public virtual void EndEffect() {
        onEffectEnded?.Invoke();
    }

    public Effect(Unit unit, string code, int duration, Cooldown cooldown) : base(unit, code, cooldown) {
        Duration = duration;
        OnMoveEnded += Decrease;
    }

    public Effect(Unit unit, string code, int duration) : base(unit, code, new Cooldown(0, 0)) {
        Duration = duration;
        OnMoveEnded += Decrease;
    }

    public Effect(Unit unit, string code, int duration, Cooldown cooldown, Action startEffect) : base(unit, code, cooldown) {
        Duration = duration;
        OnMoveEnded += Decrease;
        startEffect?.Invoke();
    }

    public Effect(Unit unit, string code, int duration, Action startEffect) : base(unit, code, new Cooldown(0, 0)) {
        Duration = duration;
        OnMoveEnded += Decrease;
        startEffect?.Invoke();
    }

    public Effect AddEndEffect(Action action) {
        onEffectEnded = action;
        return this;
    }

    private void Decrease() {
        Duration--;
        if (Duration == 0) {
            EndEffect();
            owner.Effects.Remove(this);
            OnMoveEnded = null;
        }
    }

    protected override void AddToUnit() {
        owner.AddEffect(this);
    }

    protected override SkillVisual GenerateVisual() {
        return new SkillVisual(Code, "no desc", Game.SpritesExtractor.GetEffectSprite(Code));
    }

}