using System;
using UnityEngine;
using Zenject;

public class BuyPanel : MonoBehaviour {

    [SerializeField]
    private BuyButton _buyButton;

    [Inject] 
    private UnitInfoPanel _unitInfoPanel;

    private void Start() {
        foreach (UnitType unitType in Enum.GetValues(typeof(UnitType))) {
            BuyButton buyButton = Instantiate(_buyButton, transform);
            //buyButton.Init(unitType, _unitInfoPanel);
        }
    }

}