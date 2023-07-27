using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(Button))]
public class SkillButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    [SerializeField]
    private Image _cooldownPanel;

    [SerializeField]
    private Image _image;

    private Button _button;

    private SpritesExtractor _spritesExtractor;

    [SerializeField]
    private Color _activeColor;

    [SerializeField]
    private Color _inactiveColor;

    private UsableSkill _skill;

    private void Awake() {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => {
            if (_skill == null)
                return;

            if (_skill.CanUse())
                _skill.Use();
        });
    }

    public void SetSpriteExtractor(SpritesExtractor spriteExtractor) {
        _spritesExtractor = spriteExtractor;
    }

    public void SetSkill(UsableSkill skill) {
        _skill = skill;
        _image.sprite = _skill.Visual.Icon;
        _image.color = _skill.CanUse() ? _activeColor : _inactiveColor;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        Debug.Log(_skill.Visual.Description);
    }

    public void OnPointerExit(PointerEventData eventData) {
        Debug.Log("Exited");
    }

}