using Mirror;
using Steamworks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SteamLobby : MonoBehaviour {

    #region Singleton
    public static SteamLobby Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<SteamLobby>();
            }
            return _instance;
        }
    }
    private static SteamLobby _instance;
    #endregion

    private string HostAddressKey => "HostAddress";

    [SerializeField]
    private NetworkPanel _panel;

    protected Callback<LobbyCreated_t> LobbyCreatedCallback;
    protected Callback<GameLobbyJoinRequested_t> JoinRequestedCallback;
    protected Callback<LobbyEnter_t> LobbyEnteredCallback;

    public ulong LobbyId { get; protected set; }

    private SteamNetworkManager _networkManager;

    private void Start() {
        if (!SteamManager.Initialized)
            return;

        _networkManager = GetComponent<SteamNetworkManager>();

        LobbyCreatedCallback = Callback<LobbyCreated_t>.Create(LobbyCreated);
        JoinRequestedCallback = Callback<GameLobbyJoinRequested_t>.Create(JoinRequested);
        LobbyEnteredCallback = Callback<LobbyEnter_t>.Create(LobbyEntered);
    }

    public void HostLobbyForFriends() {
        SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, 2);
    }

    public void FindMultiplayerGame() {
        SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, 2);
    }

    private void LobbyCreated(LobbyCreated_t callback) {
        if (callback.m_eResult != EResult.k_EResultOK)
            return;

        Debug.Log("Lobby created");

        SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HostAddressKey, SteamUser.GetSteamID().ToString());
        SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), "name", SteamFriends.GetPersonaName() + "'s EWA lobby");

        _panel.UpdateLobbyName();

        _networkManager.StartHost();
    }

    private void JoinRequested(GameLobbyJoinRequested_t callback) {
        Debug.Log("Request to join lobby");
        SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
    }

    private void LobbyEntered(LobbyEnter_t callback) {
        // Everyone
        LobbyId = callback.m_ulSteamIDLobby;

        // Clients only
        if (NetworkServer.active)
            return;

        _networkManager.networkAddress = SteamMatchmaking.GetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HostAddressKey);

        _networkManager.StartClient();
    }

    private void OnDisable() {
        if (Game.Mode == GameMode.Multiplayer)
            SteamMatchmaking.LeaveLobby(new CSteamID(LobbyId));
    }

}