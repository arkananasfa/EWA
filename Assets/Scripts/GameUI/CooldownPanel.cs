using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CooldownPanel : MonoBehaviour {

    [SerializeField]
    private Image _blackoutPanel;

    [SerializeField]
    private Image _cooldownCounterImage;

    [SerializeField]
    private TextMeshProUGUI _cooldownCounter;

    [SerializeField]
    private TextMeshProUGUI _cooldownCounterWithCharges;

    [SerializeField]
    private Image _chargesPanel;

    [SerializeField]
    private TextMeshProUGUI _chargesCounter;

    private Cooldown _cooldown;
    private ChargesCooldown _chargesCooldown;

    public void SetCooldown(Cooldown cooldown) {
        CancelSubscription();
        _cooldown = cooldown;
        if (cooldown is ChargesCooldown chargesCooldown) {
            _chargesCooldown = chargesCooldown;
            UpdateChargesCooldown();
            _cooldown.OnStateSet += UpdateChargesCooldown;
        } else {
            UpdateCooldown();
            _cooldown.OnStateSet += UpdateCooldown;
        }
    }

    private void UpdateChargesCooldown() {
        _chargesPanel.gameObject.SetActive(true);
        _chargesCounter.text = _chargesCooldown.ChargesCount.ToString();
        if (_cooldown.IsReady) {
            _blackoutPanel.gameObject.SetActive(false);
            _cooldownCounter.gameObject.SetActive(false);
            _cooldownCounterImage.gameObject.SetActive(false);
            if (_cooldown.Now == _cooldown.Full && _chargesCooldown.ChargesCount == _chargesCooldown.MaxCharges) {
                _cooldownCounterWithCharges.gameObject.SetActive(false);
            } else {
                _cooldownCounterWithCharges.gameObject.SetActive(true);
                _cooldownCounterWithCharges.text = _cooldown.Now.ToString();
            }
        } else {
            _blackoutPanel.gameObject.SetActive(true);
            _cooldownCounter.gameObject.SetActive(true);
            _cooldownCounter.text = _cooldown.Now.ToString();
            _cooldownCounterImage.gameObject.SetActive(true);
            _cooldownCounterImage.fillAmount = (float)_cooldown.Now / _cooldown.Full;
            _cooldownCounterWithCharges.gameObject.SetActive(false);
        }
    }

    private void UpdateCooldown() {
        _chargesPanel.gameObject.SetActive(false);
        _cooldownCounterWithCharges.gameObject.SetActive(false);
        _blackoutPanel.gameObject.SetActive(!_cooldown.IsReady);
        _cooldownCounter.gameObject.SetActive(!_cooldown.IsReady);
        _cooldownCounter.text = _cooldown.Now.ToString();
        _cooldownCounterImage.gameObject.SetActive(!_cooldown.IsReady);
        _cooldownCounterImage.fillAmount = (float)_cooldown.Now / _cooldown.Full;
    }

    private void CancelSubscription() {
        if (_cooldown == null) 
            return;
        if (_cooldown is ChargesCooldown) {
            _cooldown.OnStateSet -= UpdateChargesCooldown;
        } else {
            _cooldown.OnStateSet -= UpdateCooldown;
        }
    }

    private void OnDisable() {
        CancelSubscription();
    }

}