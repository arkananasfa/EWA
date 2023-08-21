using System;

public class Skill : IGameEventHandler {

    #region Events
    public Action OnMoveStarted { get; set; }
    public Action OnMoveEnded { get; set; }
    public Action<Unit, Unit, HPInfluence> OnPreApplyHpInfluence { get; set; }
    public Action<Unit, Unit, HPInfluence> OnHpInfluenceApplied { get; set; }
    public Action<Unit, Skill> OnUnitUsedSkill { get; set; }
    public Action<Unit> OnUnitDied { get; set; }
    public Action<Unit, Cage, Cage> OnUnitMoved { get; set; }
    #endregion

    public string Code { get; set; }
    public SkillVisual Visual { get; set; }
    public Cooldown Cooldown { get; set; }

    protected Unit owner;

    public Skill(Unit unit, string code, Cooldown cooldown) {
        owner = unit;
        Cooldown = cooldown;
        Code = code;
        Visual = GenerateVisual();
        // Todo add descs and translates for skills

        OnMoveStarted += () => cooldown.Decrease();

        AddToUnit();
    }

    public virtual bool CanUse() {
        return Cooldown.IsReady;
    }

    protected virtual SkillVisual GenerateVisual() {
        NameDescriptionJSON visualJSON = LanguageManager.GetSkillVisual(Code);
        return new SkillVisual(visualJSON.Name, visualJSON.Description, Game.SpritesExtractor.GetSkillSprite(Code));
    }

    protected virtual void AddToUnit() {
        owner.AddPassiveSkill(this);
    }

}