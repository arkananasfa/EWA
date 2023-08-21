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

    [SerializeField]
    private TextMeshProUGUI _nameText;

    [SerializeField]
    private UnitInfoPanel _unitInfoPanel;

    private Button _button;
    private int _price;

    private Player _playerSubscribed;

    private bool _isSubscribed = false;

    public void Init(UnitType unitType) {
        if (_isSubscribed) {
            UnsubscribedFromPlayer();
            Game.Loop.OnMoveStarted -= ChangeSubscribedPlayer;
        }
        gameObject.SetActive(true);

        var archiveElement = Game.UnitsArchive.GetElementByUnitType(unitType);

        _unitType = archiveElement.unitType;
        _unitImage.sprite = archiveElement.sprite;
        _price = archiveElement.price;
        _priceText.text = _price.ToString();
        _nameText.text = unitType.ToString();
        
        if (_button == null)
            _button = gameObject.GetComponent<Button>();

        _button.onClick.AddListener(() => {
            GameAction gameAction = Game.ActionBuilder.CreateBuyAction(_unitType);
            Game.CageChooseManager.SetAction(gameAction);
            Unit unit = Game.UnitsFactory.CreateUnit(_unitType);
            _unitInfoPanel.SetUnitWithoutSprite(unit);
            _unitInfoPanel.SetSprite(_unitImage.sprite);
        });

        _isSubscribed = true;
        _playerSubscribed = Game.CurrentPlayer;
        SubscribeToPlayer();
        Game.Loop.OnMoveStarted += ChangeSubscribedPlayer;
    }

    public void Hide() {
        if (_isSubscribed) {
            UnsubscribedFromPlayer();
            Game.Loop.OnMoveStarted -= ChangeSubscribedPlayer;
        }
        _isSubscribed = false;
        gameObject.SetActive(false);
    }

    private void ChangeSubscribedPlayer() {
        if (_playerSubscribed != null)
            _playerSubscribed.OnGoldChanged -= CheckIsGoldEnough;
        SubscribeToPlayer();
    }

    private void SubscribeToPlayer() {
        CheckIsGoldEnough();
        Game.CurrentPlayer.OnGoldChanged += CheckIsGoldEnough;
    }

    private void UnsubscribedFromPlayer() {
        if (_playerSubscribed != null)
            _playerSubscribed.OnGoldChanged -= CheckIsGoldEnough;
    }

    private void CheckIsGoldEnough() {
        _button.interactable = Game.CurrentPlayer.Gold >= _price && Game.Network.IsPlayersTurn();
    }

}