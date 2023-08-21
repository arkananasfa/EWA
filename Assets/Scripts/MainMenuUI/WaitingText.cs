using TMPro;
using UnityEngine;

public class WaitingText : MonoBehaviour {

    private TextMeshProUGUI _text;

    private void Start() {
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void ChangeText(string text) {
        _text.text = text;
    }

}