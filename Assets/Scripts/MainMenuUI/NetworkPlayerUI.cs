using Steamworks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NetworkPlayerUI : MonoBehaviour {

    public string PlayerName;
    public int PlayerID;
    public ulong SteamID {
        get => _steamID;
        set { 
            _steamID = value;
            if (!_isAvatarReceived)
                GetPlayerIcon();
        }

    }
    private ulong _steamID;

    protected Callback<AvatarImageLoaded_t> avatarLoadedCallback;

    public bool IsReady => _isAvatarReceived;

    [SerializeField]
    private RawImage _avatarImage;

    [SerializeField]
    private TextMeshProUGUI _usernameText;

    private bool _isAvatarReceived;

    private Animator _animator;

    private void Awake() {
        _animator = GetComponent<Animator>();
        _animator.enabled = false;
    }

    public void Start() {
        avatarLoadedCallback = Callback<AvatarImageLoaded_t>.Create(SetAvatar);
    }

    public void StartAnimation() {
        _animator.enabled = true;
    }

    private void GetPlayerIcon() {
        int imageID = SteamFriends.GetLargeFriendAvatar((CSteamID)SteamID);
        if (imageID == -1) return;
        Texture2D t = SteamService.GetSteamImageAsTexture(imageID);
        _avatarImage.texture = t;
        foreach (var poc in SteamNetworkManager.Instance.Players) {
            if (poc.SteamID == SteamID) {
                poc.Avatar = t;
            }
        }
    }

    public void SetAvatar(AvatarImageLoaded_t callback) {
        Debug.Log("Try to load avatar via callback");
        if (callback.m_steamID.m_SteamID == SteamID) {
            Texture2D t = SteamService.GetSteamImageAsTexture(callback.m_iImage);
            _avatarImage.texture = t;
            _isAvatarReceived = true;
            foreach (var poc in SteamNetworkManager.Instance.Players) {
                if (poc.SteamID == SteamID) {
                    poc.Avatar = t;
                }
            }
        }
    }

    public void SetUserName(string username) {
        PlayerName = username;
        _usernameText.text = PlayerName;
    }


}