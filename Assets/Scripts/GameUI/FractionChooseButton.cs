using UnityEngine;
using UnityEngine.UI;

public class FractionChooseButton : MonoBehaviour {

    [field:SerializeField]
    public FractionType FractionType { get; private set; }

    public FractionsPanel FractionPanel { get; set; }

    [SerializeField]
    private Color _selectedColor = new(0.5f, 0.5f, 0.5f);

    private Image _image;
    private Button _button;

    private void Awake() {
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(Choose);
    }

    private void Choose() {
        FractionPanel.ChooseFraction(FractionType);
    }

    public void SetInteractable(bool interactable) {
        Deselect();
        _button.interactable = interactable;
    }

    public void Select() {
        _image.color = _selectedColor;
    }

    public void Deselect() {
        _image.color = Color.white;
    }

}