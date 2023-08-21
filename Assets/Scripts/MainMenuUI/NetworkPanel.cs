using Mirror;
using Steamworks;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkPanel : MonoBehaviour {

    public ulong LobbyID;
    public PlayerObjectController LocalPlayerObjectController;

    public static NetworkPanel Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<NetworkPanel>();
            }
            return _instance;
        }
    }
    private static NetworkPanel _instance;

    private SteamNetworkManager Manager => SteamNetworkManager.Instance;

    [SerializeField]
    private TimerText _timerText;

    [SerializeField]
    private WaitingText _waitingText;

    [SerializeField]
    private TextMeshProUGUI _lobbyNameText;

    [SerializeField]
    private GameObject _panelObject;

    [SerializeField]
    private NetworkPlayerUI _player1UI;

    [SerializeField]
    private NetworkPlayerUI _player2UI;

    [SerializeField]
    private List<GameObject> _noPlayer2ObjectsHide;

    [SerializeField]
    private List<GameObject> _player2ObjectsHide;
    
    public void UpdateLobbyName() {
        _panelObject.SetActive(true);
        LobbyID = SteamLobby.Instance.LobbyId;
        _lobbyNameText.text = SteamMatchmaking.GetLobbyData(new CSteamID(LobbyID), "name");
    }

    public void UpdatePlayerList() {
        UpdatePlayers();
    }

    public void FindLocalPlayer() {
        LocalPlayerObjectController = GameObject.Find("LocalPlayer").GetComponent<PlayerObjectController>();
    }

    public void AddPlayerUI(PlayerObjectController playerObjectController) {
        UpdatePlayers();
    }

    public void UpdatePlayers() {
        List<PlayerObjectController> playersObjects = Manager.Players;

        if (playersObjects.Count > 0) {
            var player1 = playersObjects[0];
            _player1UI.SetUserName(player1.Name);
            _player1UI.PlayerID = player1.PlayerID;
            _player1UI.SteamID = player1.SteamID;
        }

        if (playersObjects.Count > 1) {
            var player2 = playersObjects[1];
            _player2UI.SetUserName(player2.Name);
            _player2UI.PlayerID = player2.PlayerID;
            _player2UI.SteamID = player2.SteamID;
            _noPlayer2ObjectsHide.ForEach(go => go.SetActive(true));
            _player2ObjectsHide.ForEach(go => go.SetActive(false));
        } else {
            _noPlayer2ObjectsHide.ForEach(go => go.SetActive(false));
            _player2ObjectsHide.ForEach(go => go.SetActive(true));
        }
    }

    public void StartGame() {
        Debug.Log("Game is suppose to be started! yuhu");
        GoToGameScene();
    }

    public void StartTimer(Action callback) {
        _timerText.gameObject.SetActive(true);
        _timerText.StartTimer(this);
        _timerText.SetCallback(callback);
    }

    private void GoToGameScene() {
        Manager.StartGame();
    }

    public void StartWithFriendGameSearch() {
        _panelObject.SetActive(true);
        SteamLobby.Instance.HostLobbyForFriends();
    }

    public void StartMultiplayerGameSearch() {
        _panelObject.SetActive(true);
        SteamLobby.Instance.FindMultiplayerGame();
    }

    public void LeaveLobby() {
        _panelObject.SetActive(false);
    }

}