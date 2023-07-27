using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UnitsActionsUI : MonoBehaviour {

    [SerializeField]
    private GameObject _content;

    [SerializeField]
    private SkillButton _moveButton;

    [SerializeField]
    private SkillButton _attackButton;

    [SerializeField]
    private GameObject _skillsPanel;

    private Unit _unit;
    private UnitView _unitView;

    [Inject]
    private SpritesExtractor _spritesExtractor;

    private void Awake() {
        _moveButton.SetSpriteExtractor(_spritesExtractor);
        _attackButton.SetSpriteExtractor(_spritesExtractor);
    }

    public void SetUnit(Unit unit) {
        _unit = unit;
        _unitView = unit.View;
        _moveButton.SetSkill(unit.Mover);
        _attackButton.SetSkill(_unit.Attacker);
        Show();
    }

    private void Hide() {
        _content.SetActive(false);
    }

    private void Show() {
        _content.SetActive(true);
    }

}