using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public abstract class Unit { 

    public UnitView View { get; private set; }

    public string Name { get; private set; }

    public Team Team { get; set; }
    public Cage Cage {
        get => _cage;
        set {
            if (_cage != null && _cage != value) {
                Cage from = _cage;
                _cage = value;
                _cage.Unit = this;
                Game.Loop.UnitMoved(this, from, value);
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

    public List<Skill> Skills {
        get {
            List<Skill> skills = new();
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

    public Unit(int hp, HPInfluence damage, int armor, int resistance) {
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

    public void Init(Sprite sprite, Cage cage, Team team, bool preview = false) {
        _sprite = sprite;
        if (!preview) {
            Team = team;
            Cage = cage;
            Cage.Unit = this;
            View = CreateView(Cage.View);
            View.SetUnit(this);
        }
    }

    public virtual void ApplyHPChange(Unit from, HPInfluence hpChange) {
        Game.Loop.PreApplyHpInfluence(from, this, hpChange);

        if (hpChange.IsBlocked)
            return;

        decimal actualNumber = Math.Clamp(hpChange.Value, 0, 10000);
        if (hpChange.Type == HPChangeType.Damage) {
            if (hpChange.DamageType == DamageType.Physical)
                actualNumber *= 1 - Armor;
            else if (hpChange.DamageType == DamageType.Magical)
                actualNumber *= 1 - Resistance;

            actualNumber = Math.Round(actualNumber, 1);
            HP -= actualNumber;

            hpChange.Value = actualNumber;
            Game.Loop.HpInfluenceApplied(from, this, hpChange);
        } else {
            if (hpChange.HealType == HealType.Standard)
                actualNumber = Math.Clamp(actualNumber, 0, MaxHP - HP);

            actualNumber = Math.Round(actualNumber, 1);
            HP += actualNumber;
            // TODO invoke onunithealed
        }
    }

    public virtual void Die() {
        Debug.Log($"{Name} died:(");
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

    private UnitView CreateView(CageView View) {
        UnitView view = UnityEngine.Object.Instantiate(Game.UnitViewSpritesArchive.GetUnitViewPrefab(), View.transform);
        view.SetSprite(_sprite);
        return view;
    }
}