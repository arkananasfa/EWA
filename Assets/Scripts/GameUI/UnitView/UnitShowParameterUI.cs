using TMPro;
using UnityEngine;

public class UnitShowParameterUI : MonoBehaviour {

    [SerializeField]
    private TextMeshProUGUI _text;

    public void BoundToParameter (ShowParameter showParameter) {
        showParameter.OnValueChanged += ChangeValue;
        ChangeValue(showParameter.Value);
    }

    private void ChangeValue(decimal value) {
        _text.text = value.ToString();
    }

}