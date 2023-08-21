using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class UsableSkill : Skill {

    public event Action OnUsed;

    protected Func<List<Cage>> getPossibleTargets;
    protected Action<Cage> applyEffect;
    
    protected GameActionType actionType;
    protected int skillNumber;

    protected string audioName;
    protected int soundNumber = 0;
    protected List<AudioSource> sounds;

    public UsableSkill(Unit unit, string code, Cooldown cooldown, GameActionType type, int skillNumber = 0) : base(unit, code, cooldown) {
        sounds = new();
        actionType = type;
        this.skillNumber = skillNumber;
        audioName = "";
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
        Game.Loop.UnitUsedSkill(owner, this);
        if (soundNumber < sounds.Count)
            sounds[soundNumber].Play();
    }

    public virtual void Cancel() { }

    public virtual void Use(bool network = true) {
        if (!CanUse()) return;
        Game.CageChooseManager.SetAction(CreateAction(), network);
        OnUsed?.Invoke();
    }

    public override bool CanUse() {
        return base.CanUse() && GetPossibleCages().Count>0;
    }

    public void ImplementSounds() {
        List<AudioClip> clips;
        if (audioName == "")
            clips = Game.SoundsExtractor.GetSounds(owner, this);
        else
            clips = Game.SoundsExtractor.GetSounds(owner, audioName);
        foreach (var clip in clips) {
            var source = owner.View.gameObject.AddComponent<AudioSource>();
            source.clip = clip;
            sounds.Add(source);
        }
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

    protected override SkillVisual GenerateVisual() {
        return new SkillVisual(Code, "no desc", Game.SpritesExtractor.GetSkillSprite(Code));
    }

}