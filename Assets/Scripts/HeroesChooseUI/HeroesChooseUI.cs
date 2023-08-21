using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class HeroesChooseUI : MonoBehaviour {

    #region Singleton
    public static HeroesChooseUI Instance {
        get {
            if (_instance == null) { 
                _instance = FindObjectOfType<HeroesChooseUI>();
            }
            return _instance;
        }
    }

    private static HeroesChooseUI _instance;
    #endregion

    public int CurrentHeroChooseNumber { get; set; }

    [SerializeField]
    private GameObject _content;

    [SerializeField] 
    private HeroOverview _hero1Overview;

    [SerializeField] 
    private HeroOverview _hero2Overview;

    [SerializeField]
    private HeroesChoosePanel _choosePanel;

    [SerializeField]
    private TimerView _timerView;

    private HeroOverview CurrentOverview => GetHeroOverview(CurrentHeroChooseNumber);

    private void Start() {
        if (!GlobalGameSettings.IsHeroPickingActive) {
            GetHeroOverview(1).SetHero(GlobalGameSettings.HeroType1);
            PickHero();
            GetHeroOverview(2).SetHero(GlobalGameSettings.HeroType2);
            PickHero();
            return;
        }
        _content.SetActive(true);

        _hero1Overview.gameObject.SetActive(false);
        _hero2Overview.gameObject.SetActive(false);

        CurrentHeroChooseNumber = Game.Network.IsPlayersNumber(1) ? 1 : 2;

        _choosePanel.CreateButtons();
        _choosePanel.SetLock(false);
        _choosePanel.SetOverview(CurrentOverview);

        _hero1Overview.gameObject.SetActive(true);
        if (Game.Mode == GameMode.Multiplayer) {
            _hero2Overview.gameObject.SetActive(true);
        }

        _timerView.Set(30);
        _timerView.OnTimeEnded += PickRandomHero;
    }

    public void PickHero() {
        if (Game.Mode == GameMode.HotSeat)
            PickHeroHotSeat();
        else if (Game.Mode == GameMode.Multiplayer)
            PickHeroMultiplayer();
    }

    public void SetOverviewPicked(HeroType heroType, int player) {
        if (Game.Mode == GameMode.Multiplayer) {
            if (CurrentHeroChooseNumber != player) {
                GetHeroOverview(player).SetHero(heroType);
            }
        }
    }

    private void PickHeroHotSeat() {
        if (CurrentHeroChooseNumber == 1) {
            PerformPick();

            _hero2Overview.gameObject.SetActive(true);
            _hero2Overview.SetHero(HeroType.HolyPrincess);
            CurrentHeroChooseNumber = 2;
            _choosePanel.SetOverview(CurrentOverview);
        } else {
            PerformPick();

            _choosePanel.SetLock(true);
        }
    }

    private void PickHeroMultiplayer() {
        PerformPick();
        _choosePanel.SetLock(true);
    }

    private void PerformPick() {
        CurrentOverview.HidePickButton();
        GameAction action = new() {
            Parameter = (int)GetHeroOverview(CurrentHeroChooseNumber).HeroType,
            Type = GameActionType.HeroPick,
            X = CurrentHeroChooseNumber
        };
        Game.GameActionPerformer.Perform(action);
    }

    private HeroOverview GetHeroOverview(int number) {
        return number == 1 ? _hero1Overview : _hero2Overview;
    }

    private void PickRandomHero() {
        var overview = GetHeroOverview(CurrentHeroChooseNumber);
        int random = Random.Range(0, Enum.GetNames(typeof(HeroType)).Length);
        overview.HeroType = (HeroType)random;
        PerformPick();
    }

    public void EndHeroPick() {
        Destroy(gameObject);
    }

}