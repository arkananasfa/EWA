using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour {

    [SerializeField] private Image fill;

    public void ChangeValue(float part) {
        fill.fillAmount = part;
    }

}