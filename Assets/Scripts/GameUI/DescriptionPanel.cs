using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionPanel : MonoBehaviour {

    [SerializeField]
    private GameObject _panelGameObject;

    [SerializeField]
    private Image _icon;

    [SerializeField]
    private TextMeshProUGUI _nameText;

    [SerializeField]
    private TextMeshProUGUI _descriptionText;

    [SerializeField]
    private TextMeshProUGUI _cooldownText;

    [SerializeField]
    private GameObject _cooldownPanel;

    [SerializeField]
    private Image _cooldownDownPanel;

    [SerializeField]
    private VerticalLayoutGroup _layoutGroup;

    private Transform CanvasTransfrom {
        get {
            if (_canvasTransform == null)
                _canvasTransform = GetComponentInParent<Canvas>().transform;
            return _canvasTransform;
        }
    }
    private Transform _canvasTransform;

    private void Start() {
        HidePanel();
    }

    public void SetSkill(Skill skill) {

        _panelGameObject.SetActive(true);
        _icon.sprite = skill.Visual.Icon;
        _nameText.SetText(skill.Visual.Name);
        _descriptionText.SetText(skill.Visual.Description);
        _cooldownText.SetText(skill.Cooldown.ToString());
        bool skillIsUsable = skill is UsableSkill;
        _cooldownDownPanel.gameObject.SetActive(skillIsUsable);
        _cooldownPanel.gameObject.SetActive(skillIsUsable);

        Canvas.ForceUpdateCanvases();
        _layoutGroup.enabled = false;
        _layoutGroup.enabled = true;
    }

    public void LocatePanel(RectTransform toTransform) {
        transform.SetParent(toTransform);
        transform.localPosition = new Vector2(0, 5);
        transform.SetParent(CanvasTransfrom);
    }

    public void HidePanel() {
        _panelGameObject.SetActive(false);
    }

}