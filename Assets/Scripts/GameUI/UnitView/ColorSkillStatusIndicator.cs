using UnityEngine;
using UnityEngine.UI;

public class ColorSkillStatusIndicator : MonoBehaviour {

    [SerializeField]
    private Color _canUseColor;

    [SerializeField]
    private Color _noCageToUseColor;

    [SerializeField]
    private Color _cooldownNotReadyColor;

    private Skill _skill;

    private Image _image;

    private void Awake() {
        _image = GetComponent<Image>();
        gameObject.SetActive(false);
    }

    public void SetSkill(UsableSkill skill) {
        _skill = skill;
        _skill.Cooldown.OnStateSet += SetState;
        SetState();
    }

    private void SetState() {
        _image.color = _skill.CanUse() ? _canUseColor : _skill.Cooldown.IsReady ? _noCageToUseColor : _cooldownNotReadyColor;
    }

}