public class NetworkController {

    public Team PlayersTeam { get; set; }

    public SteamNetworkManager Manager => SteamNetworkManager.Instance;

    public NetworkController() { 
        if (Game.Mode == GameMode.Multiplayer)
            PlayersTeam = Manager.GetOnlineTeam();
    }

    public bool IsUnitPlayable(Unit unit) {
        return (Game.Mode == GameMode.HotSeat || unit.Team == PlayersTeam) && unit.Team == Game.CurrentTeam;
    }

    public bool IsPlayersTurn() {
        return Game.Mode == GameMode.HotSeat || PlayersTeam == Game.CurrentTeam;
    }

    public void SendActionToOpponent(GameAction action) {
        Manager.GetOwnPlayer().SendAction(action);
    }

    public void SendMoveEndActionToOpponent(GameAction action) {
        Manager.GetOwnPlayer().SendEndMoveAction(action);
    }

    public bool IsPlayersNumber(int number) {
        return Game.Mode == GameMode.HotSeat ||
               (Game.Network.PlayersTeam == (number == 1 ? Game.Team1 : Game.Team2));
    }

}