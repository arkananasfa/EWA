using Mirror;
using Steamworks;
using UnityEngine;

public class PlayerObjectController : NetworkBehaviour {

    [SyncVar] public int ConnectionID;
    [SyncVar] public int PlayerID;
    [SyncVar] public ulong SteamID;
    [SyncVar(hook = nameof(PlayerNameUpdate))] public string Name;
    [SyncVar(hook = nameof(PlayerAvatarUpdate))] public Texture2D Avatar;

    public SteamNetworkManager Manager {
        get {
            if (_manager == null)
                _manager = NetworkManager.singleton as SteamNetworkManager;
            return _manager;
        }
    }

    private SteamNetworkManager _manager;

    private void Start() {
        DontDestroyOnLoad(gameObject);
    }

    public override void OnStartAuthority() {
        CmdSetPlayerName(SteamFriends.GetPersonaName().ToString());
        gameObject.name = "LocalPlayer";
        NetworkPanel.Instance.FindLocalPlayer();
        NetworkPanel.Instance.UpdateLobbyName();
    }

    public override void OnStartClient() {
        if (!isOwned) { 
            NetworkClient.RegisterHandler<NetworkGameAction>(TryPerformAction);
            NetworkClient.RegisterHandler<MoveEndGameAction>(TryPerformMoveEnd);
        }
        GlobalGameSettings.GameMode = GameMode.Multiplayer;
        Manager.Players.Add(this);
        NetworkPanel.Instance.UpdateLobbyName();
        NetworkPanel.Instance.AddPlayerUI(this);
        if (Manager.Players.Count == 2) {
            if (isOwned) {
                CmdSetTimer();
            } else {
                CmdSetRules(GlobalGameSettings.Player1.MaxGold,
                            GlobalGameSettings.Player1.GoldPerRound,
                            GlobalGameSettings.Player1.StartGold,
                            GlobalGameSettings.Player1.MaxHP);
            }
        }
    }

    public void SendAction(GameAction action) {
        var netAction = action.ToNetwork();
        CmdSendAction(netAction);
    }

    [Command]
    public void CmdSendAction(NetworkGameAction netAction) {
        NetworkServer.SendToAll(netAction);
    }

    public void SendEndMoveAction(GameAction action) {
        var netAction = action.ToMoveEndNetwork();
        CmdSendEndMoveAction(netAction);
    }

    [Command]
    public void CmdSendEndMoveAction(MoveEndGameAction netAction) {
        NetworkServer.SendToAll(netAction);
    }

    public void TryPerformAction(NetworkGameAction action) {
        var gameAction = new GameAction(action);
        if (gameAction.Type == GameActionType.HeroPick && !Game.Network.IsPlayersNumber(gameAction.X))
            Game.GameActionPerformer.Perform(gameAction);
        else if (!Game.Network.IsPlayersTurn() && gameAction.Type != GameActionType.HeroPick)
            Game.GameActionPerformer.Perform(gameAction);
    }

    public void TryPerformMoveEnd(MoveEndGameAction action) {
        if (Game.CurrentTeam != (action.teamNumber == 1 ? Game.Team1 : Game.Team2))
            Game.Loop.MoveEnded();
    }

    [Command]
    public void CmdSetRules(int MaxGold, int GoldPerRound, int StartGold, int MaxHP) {
        RpcSetRules(MaxGold, GoldPerRound, StartGold, MaxHP);
    }

    [ClientRpc]
    public void RpcSetRules(int MaxGold, int GoldPerRound, int StartGold, int MaxHP) {
        GlobalGameSettings.Player1.MaxGold = MaxGold;
        GlobalGameSettings.Player1.GoldPerRound = GoldPerRound;
        GlobalGameSettings.Player1.StartGold = StartGold;
        GlobalGameSettings.Player1.MaxHP = MaxHP;
        GlobalGameSettings.Player2.MaxGold = MaxGold;
        GlobalGameSettings.Player2.GoldPerRound = GoldPerRound;
        GlobalGameSettings.Player2.StartGold = StartGold;
        GlobalGameSettings.Player2.MaxHP = MaxHP;
        Debug.Log("My rules has been changed");
    }

    public override void OnStopClient() {
        Manager.Players.Remove(this);
    }

    [Command]
    private void CmdSetPlayerName(string playerName) {
        PlayerNameUpdate(Name, playerName);
        NetworkPanel.Instance.UpdatePlayers();
    }

    [Command]
    private void CmdSetTimer() {
        RpcStartTimer();
    }

    [Command]
    public void CmdStartGame() {
        SteamNetworkManager.Instance.StartGame();
    }

    [ClientRpc]
    public void RpcStartTimer() {
        Debug.Log("RpcStartTimer");

        NetworkPanel.Instance.StartTimer(() => this.CmdStartGame());
    }

    public void PlayerNameUpdate(string oldValue, string newValue) { 
        if (isServer) {
            Name = newValue;
        } 
        if (isClient) {
            NetworkPanel.Instance.UpdatePlayers();
        }
    }

    public void PlayerAvatarUpdate(Texture2D oldValue, Texture2D newValue) {
        if (isServer) {
            Avatar = newValue;
        }
    }

}