using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit { 

    public UnitView View { get; private set; }

    public string Name { get; private set; }

    public Player Player { get; set; }
    public Team Team { get; set; }
    public Cage Cage {
        get => _cage;
        set {
            if (_cage != null && _cage != value) {
                if (value.Y == Team.Opponent.HomeY) {
                    View.Move(value);
                    View.RemoveUnit();
                    Cage.Unit = null;
                    Team.RemoveUnit(this);
                    Player.Opponent.HP -= (int)Damage.Value;
                    Game.Loop.UnitDied(this);
                } else {
                    Cage from = _cage;
                    from.Unit = null;
                    _cage = value;
                    _cage.Unit = this;
                    Game.Loop.UnitMoved(this, from, value);
                    View.Move(value);
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

    public Unit(int hp, string name, HPInfluence damage, int armor, int resistance) {
        ActiveSkills = new();
        PassiveSkills = new();
        Effects = new();

        Name = name;
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
        }
    }

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

    public virtual void Die() {
        Debug.Log($"{Name} died:(");
        Cage.Unit = null;
        Team.RemoveUnit(this);
        UnityEngine.Object.Destroy(View.gameObject);
        Game.Loop.UnitDied(this);
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

    private UnitView CreateView(CageView View) {
        UnitView view = UnityEngine.Object.Instantiate(Game.UnitsArchive.GetUnitViewPrefab(), View.transform);
        view.SetSprite(_sprite);
        return view;
    }
}