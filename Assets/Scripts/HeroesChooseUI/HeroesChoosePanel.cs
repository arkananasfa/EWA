using System;
using System.Collections.Generic;
using UnityEngine;

public class HeroesChoosePanel : MonoBehaviour {

    [SerializeField]
    private HeroChooseButton _chooseButtonPrefab;

    private List<HeroChooseButton> _buttons;

    public void CreateButtons() {
        _buttons = new();
        foreach (HeroType heroType in Enum.GetValues(typeof(HeroType))) {
            HeroChooseButton heroIcon = Instantiate(_chooseButtonPrefab, transform);
            heroIcon.Init(heroType);
            _buttons.Add(heroIcon);
        }
    }

    public void SetLock(bool isLock) {
        if (_buttons != null)
            _buttons.ForEach(button => button.SetLock(isLock));
    }

    public void SetOverview(HeroOverview overview) {
        _buttons.ForEach(button => button.SetOverview(overview));
    }

}