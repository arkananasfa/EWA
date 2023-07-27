using UnityEngine;
using UnityEngine.UI;

public class UnitView : MonoBehaviour {

    public Unit Unit => _context;

    [SerializeField]
    private ColorSkillStatusIndicator _moveIndicator;

    [SerializeField]
    private ColorSkillStatusIndicator _attackIndicator;

    private Unit _context;
    private Image _image;

    private void Awake() {
        _image = GetComponent<Image>();
    }

    public void SetUnit(Unit unit) {
        _context = unit;

        SetupIndicators();
    }

    public void SetSprite (Sprite sprite) {
        _image.sprite = sprite;
    }

    protected virtual void SetupIndicators() {
        _moveIndicator.gameObject.SetActive(true);
        _moveIndicator.SetSkill(Unit.Mover);
        _attackIndicator.gameObject.SetActive(true);
        _attackIndicator.SetSkill(Unit.Attacker);
    }

}