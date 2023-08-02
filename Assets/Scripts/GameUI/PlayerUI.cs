using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

    [SerializeField]
    private Image _avatarImage;

    [SerializeField]
    private Image _avatarBorders;

    [SerializeField]
    private TextMeshProUGUI _nameText;

    [SerializeField]
    private TextMeshProUGUI _goldText;

    [SerializeField]
    private TextMeshProUGUI _hpText;

    private Player _player;

    public void SetPlayer(Player player) {
        _player = player;
        _nameText.text = _player.Name;
        _avatarImage.sprite = Resources.Load<Sprite>($"TestAvas/{_player.Name}");
        UpdateUI();
        _player.OnGoldChanged += UpdateUI;
        _player.OnHPChanged += UpdateUI;
    }

    public void UpdateUI() {
        _hpText.text = _player.HP.ToString();
        _goldText.text = _player.Gold.ToString();
    }

}