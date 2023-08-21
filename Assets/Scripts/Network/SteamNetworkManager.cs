using Mirror;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SteamNetworkManager : NetworkManager {

    public static SteamNetworkManager Instance {
        get {
            if (_instance == null) {
                _instance = singleton as SteamNetworkManager;
            }
            return _instance;
        }
    }
    private static SteamNetworkManager _instance;

    [SerializeField] private PlayerObjectController _playerPrefab;
    public List<PlayerObjectController> Players = new List<PlayerObjectController>();

    public override void OnServerAddPlayer(NetworkConnectionToClient conn) {
        PlayerObjectController newPlayerInstance = Instantiate(_playerPrefab);
        newPlayerInstance.ConnectionID = conn.connectionId;
        newPlayerInstance.PlayerID = Players.Count + 1;
        newPlayerInstance.SteamID = (ulong)SteamMatchmaking.GetLobbyMemberByIndex((CSteamID)SteamLobby.Instance.LobbyId, Players.Count);

        NetworkServer.AddPlayerForConnection(conn, newPlayerInstance.gameObject);
    }

    public void StartGame(string sceneName = "Game") {

        ServerChangeScene(sceneName);
    }

    public PlayerObjectController GetOwnPlayer() {
        return Players.Where(p => p.isOwned).FirstOrDefault();
    }

    public PlayerObjectController GetOtherPlayer() {
        return Players.Where(p => !p.isOwned).FirstOrDefault();
    }

    public Team GetOnlineTeam() {
        return GetOwnPlayer().PlayerID == 1 ? Game.Team1 : Game.Team2;
    }

}