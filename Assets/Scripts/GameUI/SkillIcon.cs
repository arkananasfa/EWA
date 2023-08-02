using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillIcon : MonoBehaviour {

    [SerializeField]
    private Image _image;

    [SerializeField]
    private TextMeshProUGUI _label;

    [SerializeField]
    private Color _activeSkillLabelColor;

    [SerializeField]
    private Color _effectCounterLabelColor;

    public void SetSkill(Skill skill) {
        _image.sprite = skill.Visual.Icon;
        if (skill is ActiveSkill activeSkill) {
            _label.color = _activeSkillLabelColor;
            _label.text = "A";
        } else if (skill is Effect effect) {
            _label.color = _effectCounterLabelColor;
            _label.text = effect.Duration.ToString();
        }
    }

}