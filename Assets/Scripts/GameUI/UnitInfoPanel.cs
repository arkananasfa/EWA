using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitInfoPanel : MonoBehaviour {

    [SerializeField] private GameObject _panelObject;

    [SerializeField]
    private Image _unitImage;

    [SerializeField]
    private TextMeshProUGUI _nameText;

    [SerializeField]
    private TextMeshProUGUI _hPText;
    [SerializeField] 
    private TextMeshProUGUI _damageText;
    [SerializeField] 
    private TextMeshProUGUI _armorText;
    [SerializeField] 
    private TextMeshProUGUI _resistanceText;

    [SerializeField]
    private Image _damageTypeImage;
    [SerializeField]
    private Image _rangeTypeImage;

    private void Start() {
        ChangeVisibility(false);
    }

    public void SetUnit(Unit unit) {
        SetUnitWithoutSprite(unit);
        SetSprite(unit.GetSprite());
    }

    public void SetUnitWithoutSprite(Unit unit) {
        ChangeVisibility(true);

        _nameText.text = unit.Name;
        _hPText.text = $"{unit.HP}/{unit.MaxHP}";
        _damageText.text = unit.Damage.Value.ToString();
        _armorText.text = unit.Armor.ToString();
        _resistanceText.text = unit.Resistance.ToString();
    }

    public void SetSprite(Sprite sprite) {
        _unitImage.sprite = sprite;
    }

    private void ChangeVisibility(bool isVisible) {
        _panelObject.SetActive(isVisible);
    }

    
}