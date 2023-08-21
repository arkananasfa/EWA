using System.Collections.Generic;
using UnityEngine;

public class FractionsPanel : MonoBehaviour {

    [SerializeField]
    private UnitsShop _shop;

    [SerializeField]
    private List<FractionChooseButton> _buttons;

    private FractionType _type;

    private void Start() {
        Game.Loop.OnMoveStarted += ChooseInteractable;
        ChooseFraction(Game.Player1.AllowedFractions[0]);
        foreach (var button in _buttons) {
            button.FractionPanel = this;
        }
    }

    public void ChooseInteractable() {
        foreach (var button in _buttons)
            button.SetInteractable(Game.CurrentPlayer.AllowedFractions.Contains(button.FractionType));
    }

    public void ChooseFraction(FractionType type) {
        foreach (var button in _buttons) {
            if (button.FractionType == _type)
                button.Deselect();
        }
        _type = type;
        foreach (var button in _buttons) {
            if (button.FractionType == _type)
                button.Select();
        }
        _shop.SetFraction(_type);
    }

}