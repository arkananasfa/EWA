using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour {

    private UnitType _unitType;

    [SerializeField]
    private Image _unitImage;

    [SerializeField]
    private TextMeshProUGUI _priceText;

    private UnitInfoPanel _unitInfoPanel;

    private Button _button;
    private int _price;

    private Player _playerSubscribed;

    public void Init(UnitType unitType, UnitInfoPanel infoPanel) {
        var archiveElement = Game.UnitsArchive.GetElementByUnitType(unitType);

        _unitType = archiveElement.unitType;
        _unitImage.sprite = archiveElement.sprite;
        _price = archiveElement.price;
        _priceText.text = _price.ToString();

        _unitInfoPanel = infoPanel;
        _button = gameObject.AddComponent<Button>();
        _button.onClick.AddListener(() => {
            GameAction gameAction = Game.ActionBuilder.CreateBuyAction(_unitType);
            Game.CageChooseManager.SetAction(gameAction);
            Unit unit = Game.UnitsFactory.CreateUnit(_unitType);
            _unitInfoPanel.SetUnitWithoutSprite(unit);
            _unitInfoPanel.SetSprite(_unitImage.sprite);
        });
        _playerSubscribed = Game.CurrentPlayer;
        SubscribeToPlayer();
        Game.Loop.OnMoveStarted += ChangeSubscribedPlayer;
    }

    private void ChangeSubscribedPlayer() {
        _playerSubscribed.OnGoldChanged -= CheckIsGoldEnough;
        SubscribeToPlayer();
    }

    private void SubscribeToPlayer() {
        CheckIsGoldEnough();
        Game.CurrentPlayer.OnGoldChanged += CheckIsGoldEnough;
    }

    private void CheckIsGoldEnough() {
        _button.interactable = Game.CurrentPlayer.Gold >= _price;
    }

}