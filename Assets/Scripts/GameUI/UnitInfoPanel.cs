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
    private TextMeshProUGUI _moveDistanceText;
    [SerializeField]
    private TextMeshProUGUI _attackDistanceText;

    [SerializeField]
    private Image _damageTypeImage;
    [SerializeField]
    private Image _attackTypeImage;
    [SerializeField]
    private Image _moveTypeImage;

    [SerializeField]
    private Transform _skillsIconsParent;
    [SerializeField]
    private Transform _effectIconsParent;

    [SerializeField] private SkillIcon _skillIconPrefab;

    [SerializeField]
    private Sprite _physicalDamageSprite;
    [SerializeField]
    private Sprite _magicalDamageSprite;

    private Unit _unit;
    private bool _hidden;

    private void OnEnable() {
        Game.Loop.OnUnitDied += HideIfCurrentUnitDied;
        Game.Loop.OnBattleActionHappened += UpdatePanel;
    }

    private void OnDisable() {
        Game.Loop.OnUnitDied -= HideIfCurrentUnitDied;
        Game.Loop.OnBattleActionHappened -= UpdatePanel;
    }

    private void Start() {
        ChangeVisibility(false);
    }

    public void SetUnit(Unit unit) {
        _unit = unit;

        SetUnitWithoutSprite(unit);
        SetSprite(unit.View.Sprite);
    }

    public void SetUnitWithoutSprite(Unit unit, bool overview = false) {
        Show();

        _nameText.text = unit.Name;
        _hPText.text = $"{unit.HP}/{unit.MaxHP}";
        _damageText.text = unit.Damage.Value.ToString();
        _armorText.text = unit.Armor.ToString();
        _resistanceText.text = unit.Resistance.ToString();
        _moveDistanceText.text = unit.Mover.GetDistance().ToString();
        _attackDistanceText.text = unit.Attacker.GetDistance().ToString();

        _damageTypeImage.sprite = unit.Damage.DamageType == DamageType.Physical ? _physicalDamageSprite : _magicalDamageSprite;
        _attackTypeImage.sprite = unit.Attacker.Visual.Icon;
        _moveTypeImage.sprite = unit.Mover.Visual.Icon;

        foreach (Transform child in _skillsIconsParent) {
            Destroy(child.gameObject); 
        }

        foreach (Transform child in _effectIconsParent) {
            Destroy(child.gameObject);
        }

        foreach (var skill in unit.BasicSkills) {
            var skillIcon = Instantiate(_skillIconPrefab, _skillsIconsParent);
            skillIcon.SetSkill(skill, overview);
        }

        foreach (var effect in unit.Effects) {
            var effectIcon = Instantiate(_skillIconPrefab, _effectIconsParent);
            effectIcon.SetSkill(effect);
        }
    }

    public void SetSprite(Sprite sprite) {
        _unitImage.sprite = sprite;
    }

    private void Show() {
        ChangeVisibility(true);
    }

    private void Hide() {
        ChangeVisibility(false);
    }

    private void HideIfCurrentUnitDied (Unit unit) {
        if (unit == _unit) {
            Hide();
        }
    }

    private void ChangeVisibility(bool isVisible) {
        _panelObject.SetActive(isVisible);
        _hidden = !isVisible;
    }

    private void UpdatePanel() {
        if (_unit != null && !_hidden) {
            SetUnit(_unit);
        }
    }
    
}