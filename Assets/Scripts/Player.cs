using System;
using UnityEngine;

public class Player {

    public event Action OnGoldChanged;
    public event Action OnHPChanged;

    private static int MaxGold;
    private static int MaxHP;

    public string Name { get; set; }
    public Team Team { get; set; }

    public Player Opponent => this == Game.Player1 ? Game.Player2 : Game.Player1;

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

    public Player(string name) {
        Name = name;

        MaxGold = GlobalGameSettings.MaxGold;
        MaxHP = GlobalGameSettings.MaxHP;
        Gold = GlobalGameSettings.StartGold;
        HP = MaxHP;
    }

}