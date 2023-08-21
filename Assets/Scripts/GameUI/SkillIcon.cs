using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    [SerializeField]
    private Image _image;

    [SerializeField]
    private TextMeshProUGUI _cooldownText;

    [SerializeField]
    private TextMeshProUGUI _chargesText;

    [SerializeField]
    private GameObject _chargesPanel;

    [SerializeField]
    private GameObject _cooldownPanel;

    [SerializeField]
    private GameObject _darkPanel;

    private Skill _currentSkill;
    private RectTransform _rectTransform;

    private void Awake() {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void SetSkill(Skill skill, bool overview = false) {
        _currentSkill = skill;
        _image.sprite = skill.Visual.Icon;
        _cooldownPanel.SetActive(false);
        _chargesPanel.SetActive(false);
        _darkPanel.SetActive(false);
        if (skill is ActiveSkill activeSkill) {
            _cooldownPanel.SetActive(true);
            if (skill.Cooldown.Now > 0) {
                _cooldownText.text = skill.Cooldown.Now.ToString();
            } else {
                _cooldownText.text = "A";
            }
            if (skill.Cooldown is ChargesCooldown chargesCooldown) {
                _chargesPanel.SetActive(true);
                _chargesText.text = chargesCooldown.ChargesCount.ToString();
            }
            if (!overview)
                _darkPanel.SetActive(!skill.CanUse());
        } else if (skill is Effect effect) {
            _cooldownPanel.SetActive(true);
            _cooldownText.text = effect.Duration.ToString();
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        Game.DescriptionPanel.LocatePanel(_rectTransform);
        Game.DescriptionPanel.SetSkill(_currentSkill);
    }

    public void OnPointerExit(PointerEventData eventData) {
        Game.DescriptionPanel.HidePanel();
    }

}