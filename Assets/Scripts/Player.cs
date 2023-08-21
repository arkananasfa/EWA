using System;
using System.Collections.Generic;
using UnityEngine;

public class Player {

    public event Action OnGoldChanged;
    public event Action OnHPChanged;

    public int MaxGold { get; set; }

    public PlayerUI UI;

    public int GoldPerRound { get; set; }
    public int MaxHP { get; set; }

    public bool IsPicked { get; set; }
    public int HeroStartCageX { get; set; }
    public int HeroStartCageY { get; set; }

    public List<FractionType> AllowedFractions { get; set; }

    public string Name { get; set; }
    public Team Team { get; set; }

    public Player Opponent => this == Game.Player1 ? Game.Player2 : Game.Player1;

    public int StartGold {
        get => _startGold;
        set {
            _startGold = value;
            Gold = value;
        }
    }

    public int Gold {
        get => _gold;
        set {
            _gold = Mathf.Clamp(value, 0, MaxGold);
            OnGoldChanged?.Invoke();
        }
    }

    public int HP {
        get => _hp;
        set {
            _hp = Mathf.Clamp(value, 0, MaxHP);
            OnHPChanged?.Invoke();
        }
    }

    private int _gold;
    private int _hp;
    private int _startGold;

    public Player(string name) {
        Name = name;

        MaxGold = 200;
        MaxHP = 100;
        StartGold = 50;
        GoldPerRound = 50;
        Gold = StartGold;
        HP = MaxHP;
        AllowAllFractions();
    }

    public void AllowAllFractions() {
        AllowedFractions = new();
        foreach (var fractionType in Enum.GetValues(typeof(FractionType))) {
            AllowedFractions.Add((FractionType)fractionType);
        }
    }

    public void EndGame() {
        OnHPChanged = null;
        OnGoldChanged = null;
    }

}