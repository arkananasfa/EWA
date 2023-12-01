using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Unit {

    public UnitView View { get; protected set; }

    public string Code { get; private set; }
    public string Name { get; protected set; }

    public Player Player { get; set; }
    public Team Team { get; set; }
    public Cage Cage {
        get => _cage;
        set {
            if (_cage != null && _cage != value) {
                if (value == null)
                    return;
                if (value.Y == Team.Opponent.HomeY) {
                    Die(true);
                    Player.Opponent.HP -= (int)Damage.Value;
                    return;
                } else {
                    Cage from = _cage;
                    _cage = value;
                    _cage.Unit = this;
                    if (from.Unit == this)
                        from.Unit = null;
                    Game.Loop.UnitMoved(this, from, value);
                }
            }
            _cage = value;
        }
    }

    public decimal HP {
        get => _hp;
        set {
            _hp = value;
            if (_hp <= 0) {
                Die();
            }
        }
    }

    public decimal MaxHP { get; private set; }
    public HPInfluence Damage { get; set; }
    public int Armor { get; set; }
    public int Resistance { get; set; }
    
    public List<ActiveSkill> ActiveSkills { get; set; }
    public List<Skill> PassiveSkills { get; set; }
    public List<Effect> Effects { get; set; }

    public List<Skill> BasicSkills {
        get {
            List<Skill> skills = new();
            skills.AddRange(ActiveSkills);
            skills.AddRange(PassiveSkills);
            return skills;
        }
    }

    public List<Skill> Skills {
        get {
            List<Skill> skills = new() {
                Mover,
                Attacker
            };
            skills.AddRange(ActiveSkills);
            skills.AddRange(PassiveSkills);
            skills.AddRange(Effects);
            return skills;
        }
    }

    public BaseUnitMover Mover { get; set; }
    public BaseUnitAttacker Attacker { get; set; }

    private Sprite _sprite;
    private Cage _cage;
    private decimal _hp;

    private AudioSource _takeDamageSound;
    private AudioSource _deathSound;

    public Unit(int hp, string name, HPInfluence damage, int armor, int resistance) {
        ActiveSkills = new();
        PassiveSkills = new();
        Effects = new();

        Name = name;
        Code = name;
        MaxHP = hp;
        HP = hp;
        Damage = damage;
        Armor = armor;
        Resistance = resistance;
    }
    
    public Unit SetAttacker(BaseUnitAttacker attacker) {
        Attacker = attacker;
        return this;
    }

    public Unit SetMover(BaseUnitMover mover) {
        Mover = mover;
        return this;
    }

    public Unit AddActiveSkill(ActiveSkill skill) {
        ActiveSkills.Add(skill);
        return this;
    }

    public Unit AddPassiveSkill(Skill skill) {
        PassiveSkills.Add(skill);
        return this;
    }

    public Unit AddEffect(Effect effect) {
        Effects.Add(effect);
        return this;
    }

    public void Init(Sprite sprite, Cage cage, Player player, bool preview = false) {
        _sprite = sprite;
        if (!preview) {
            Player = player;
            Team = player.Team;
            Team.AddUnit(this);
            Cage = cage;
            Cage.Unit = this;
            View = CreateView(Cage.View);
            View.SetUnit(this);
            CreateSounds();
            AfterInit();
        }
    }

    public virtual void AfterInit() {}

    public virtual void ApplyHPChange(Unit from, HPInfluence hpChange) {
        HPInfluence hpChangeCopy = hpChange.Copy();
        Game.Loop.PreApplyHpInfluence(from, this, hpChangeCopy);

        if (hpChangeCopy.IsBlocked)
            return;

        decimal actualNumber = Math.Clamp(hpChangeCopy.Value, 0, 10000);
        if (hpChangeCopy.Type == HPChangeType.Damage) {
            if (hpChangeCopy.DamageType == DamageType.Physical)
                actualNumber -= actualNumber * Armor / 10;
            else if (hpChangeCopy.DamageType == DamageType.Magical)
                actualNumber -= actualNumber * Resistance / 10;

            actualNumber = Math.Round(actualNumber, 1);
            HP -= actualNumber;

            if (_takeDamageSound != null)
                _takeDamageSound.Play();
            
            hpChangeCopy.Value = actualNumber;
            Game.Loop.HpInfluenceApplied(from, this, hpChangeCopy);
        } else {
            if (hpChangeCopy.HealType == HealType.Standard)
                actualNumber = Math.Clamp(actualNumber, 0, MaxHP - HP);

            actualNumber = Math.Round(actualNumber, 1);
            HP += actualNumber;
            // TODO invoke onunithealed
        }
    }

    public virtual void Die(bool byLastLine = false) {
        if (_deathSound != null)
            _deathSound.Play();

        Cage.Unit = null;
        View.RemoveUnit();
        Team.RemoveUnit(this);
        if (!byLastLine)
            Game.Loop.UnitDied(this);
    }

    public virtual void SetParameter(ShowParameter parameter) {
        if (View != null) {
            View.SetParameterToShow(parameter);
        }
    }

    public Sprite GetSprite() {
        return _sprite;
    }

    public bool IsTeammate(Unit unit) {
        return unit.Team == Team;
    }

    public bool HasEffect(string effectName) {
        Effect effect = Effects.Find(e => e.Code == effectName);
        return effect != null;
    }

    public void TryRemoveEffect(string effectName) {
        if (!HasEffect(effectName))
            return;

        Effect effect = Effects.Where(e => e.Code == effectName).First();
        effect.EndEffect();
    }

    public void UseDispel(Effect.Power power, Effect.DispelType dispelType) {
        List<Effect> effectsToDispel = new();
        foreach (Effect effect in Effects) {
            if (effect.PowerType <= power) {
                if (dispelType == Effect.DispelType.Both ||
                   (effect.PurposeType == Effect.Purpose.Good && dispelType == Effect.DispelType.Good) ||
                   (effect.PurposeType == Effect.Purpose.Bad && dispelType == Effect.DispelType.Bad)) 
                {
                    effectsToDispel.Add(effect);
                }
            }
        }
        foreach (Effect effect in effectsToDispel) {
            effect.EndEffect();
        }
    }

    protected virtual void CreateSounds() {
        var takeDamageClips = Game.SoundsExtractor.GetSounds(this, "TakeDamage");
        if (takeDamageClips.Count>0) {
            _takeDamageSound = View.gameObject.AddComponent<AudioSource>();
            _takeDamageSound.clip = takeDamageClips[0];
        }
        var deathClips = Game.SoundsExtractor.GetSounds(this, "Death");
        if (deathClips.Count > 0) {
            _deathSound = View.gameObject.AddComponent<AudioSource>();
            _deathSound.clip = deathClips[0];
        }
        Mover.ImplementSounds();
        Attacker.ImplementSounds();
        ActiveSkills.ForEach(skill => skill.ImplementSounds());
    }

    private UnitView CreateView(CageView View) {
        UnitView view = UnityEngine.Object.Instantiate(Game.UnitsArchive.GetUnitViewPrefab(), View.transform);
        view.SetSprite(_sprite);
        return view;
    }
}