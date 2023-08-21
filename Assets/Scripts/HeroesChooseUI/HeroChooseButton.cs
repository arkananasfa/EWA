using System;
using UnityEngine;
using UnityEngine.UI;

public class HeroChooseButton : MonoBehaviour {

    [SerializeField]
    private Image _icon;

    private HeroOverview _heroOverview;

    private Button _button;

    private void Awake() {
        _button = GetComponent<Button>();
    }

    public void Init(HeroType heroType) { 
        _icon.sprite = Game.HeroesArchive.GetSpriteByUnitType(heroType);
        _button.onClick.AddListener(() => {
            _heroOverview.SetHero(heroType);
        });
    }

    public void SetLock(bool isLock) {
        _button.interactable = !isLock;
    }

    public void SetOverview(HeroOverview overview) {
        _heroOverview = overview;
    }
}