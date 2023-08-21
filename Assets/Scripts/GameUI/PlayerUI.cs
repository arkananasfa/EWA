using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

    [SerializeField]
    private RawImage _avatarImage;

    [SerializeField]
    private TextMeshProUGUI _nameText;

    [SerializeField]
    private TextMeshProUGUI _goldText;

    [SerializeField]
    private TextMeshProUGUI _hpText;

    [SerializeField]
    private GameObject CurrentMoveArrow;

    private Player _player;

    public void SetPlayer(Player player) {
        _player = player;
        _player.UI = this;
        if (Game.Mode == GameMode.Multiplayer) {
            var poc = player.Team == Game.Network.PlayersTeam ? SteamNetworkManager.Instance.GetOwnPlayer() : SteamNetworkManager.Instance.GetOtherPlayer();
            _nameText.text = poc.Name;
            if (poc.Avatar != null)
                _avatarImage.texture = poc.Avatar;
        } else {
            _nameText.text = _player.Name;
        }
        
        UpdateUI();
        UpdateCurrentMoveArrow();
        _player.OnGoldChanged += UpdateUI;
        _player.OnHPChanged += UpdateUI;
        Game.Loop.OnMoveStarted += UpdateCurrentMoveArrow;
    }

    public void SetAvatar(Sprite sprite) {
        _avatarImage.texture = sprite.texture;
    }

    public void UpdateUI() {
        _hpText.text = _player.HP.ToString();
        _goldText.text = _player.Gold.ToString();
    }

    private void UpdateCurrentMoveArrow() {
        CurrentMoveArrow.SetActive(Game.CurrentPlayer == _player);
    }

}