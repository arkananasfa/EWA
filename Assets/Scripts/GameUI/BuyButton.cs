using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour {

    private UnitType _unitType;

    [SerializeField]
    private Image _unitImage;

    private UnitInfoPanel _unitInfoPanel;

    public void Init(UnitType unitType, UnitInfoPanel infoPanel) {
        _unitInfoPanel = infoPanel;
        _unitType = unitType;

        _unitImage.sprite = Game.UnitViewSpritesArchive.GetSpriteByUnitType(_unitType);
        Button button = gameObject.AddComponent<Button>();
        button.onClick.AddListener(() => {
            GameAction gameAction = Game.ActionBuilder.CreateBuyAction(_unitType);
            Game.CageChooseManager.SetAction(gameAction);
            Unit unit = Game.UnitsFactory.CreateUnit(_unitType);
            _unitInfoPanel.SetUnitWithoutSprite(unit);
            _unitInfoPanel.SetSprite(_unitImage.sprite);
        });
    }

}