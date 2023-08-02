using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(Button))]
public class SkillButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    [SerializeField]
    private CooldownPanel _cooldownPanel;

    [SerializeField]
    private Image _image;

    [SerializeField]
    private Color _activeColor;

    [SerializeField]
    private Color _inactiveColor;

    private Button _button;

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

    public void SetSkill(UsableSkill skill) {
        if (_button == null)
            _button = GetComponent<Button>();

        _skill = skill;
        _image.sprite = _skill.Visual.Icon;
        _image.color = _skill.CanUse() ? _activeColor : _inactiveColor;
        _button.interactable = _skill.CanUse();
        _cooldownPanel.SetCooldown(skill.Cooldown);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        // TODO Show description
    }

    public void OnPointerExit(PointerEventData eventData) {
        // TODO Hide description
    }

}