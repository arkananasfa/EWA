using System;
using UnityEngine;

public class Effect : Skill {

    public EffectVisual EffectVisual;
    public Power PowerType;
    public Purpose PurposeType;

    public int Duration { get; set; }

    private Action onEffectEnded;

    public virtual void EndEffect() {
        onEffectEnded?.Invoke();
        EffectVisual?.End();
        owner.Effects.Remove(this);
        OnMoveEnded = null;
    }

    public Effect(Unit unit, string code, int duration, Power power, Purpose purpose, Cooldown cooldown) : base(unit, code, cooldown) {
        PowerType = power;
        PurposeType = purpose;
        Duration = duration;
        OnMoveEnded += Decrease;
    }

    public Effect(Unit unit, string code, int duration, Power power, Purpose purpose) : base(unit, code, new Cooldown(0, 0)) {
        PowerType = power;
        PurposeType = purpose;
        Duration = duration;
        OnMoveEnded += Decrease;
    }

    public Effect(Unit unit, string code, int duration, Power power, Purpose purpose, Cooldown cooldown, Action startEffect) : base(unit, code, cooldown) {
        PowerType = power;
        PurposeType = purpose;
        Duration = duration;
        OnMoveEnded += Decrease;
        startEffect?.Invoke();
    }

    public Effect(Unit unit, string code, int duration, Power power, Purpose purpose, Action startEffect) : base(unit, code, new Cooldown(0, 0)) {
        PowerType = power;
        PurposeType = purpose;
        Duration = duration;
        OnMoveEnded += Decrease;
        startEffect?.Invoke();
    }

    public Effect AddEndEffect(Action action) {
        onEffectEnded = action;
        return this;
    }

    public Effect WithVisual() {
        GameObject go = UnityEngine.Object.Instantiate(Game.SpritesExtractor.GetEffectVisual(Code), owner.View.transform);
        EffectVisual = go.GetComponent<EffectVisual>();
        EffectVisual.StartIn();
        return this;
    }

    public void UseVisual() {
        EffectVisual?.Use();
    }

    private void Decrease() {
        Duration--;
        if (Duration == 0)
            EndEffect();
    }

    protected override void AddToUnit() {
        owner.AddEffect(this);
    }

    protected override SkillVisual GenerateVisual() {
        NameDescriptionJSON visualJSON = LanguageManager.GetSkillVisual(Code+"Effect");
        return new SkillVisual(visualJSON.Name, visualJSON.Description, Game.SpritesExtractor.GetEffectSprite(Code));
    }

    public enum Power {
        Weak,
        Strong,
        Undispellable
    }

    public enum Purpose {
        Good,
        Bad
    }

    public enum DispelType {
        Good,
        Bad,
        Both
    }

}