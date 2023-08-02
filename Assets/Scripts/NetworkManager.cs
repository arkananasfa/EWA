public class NetworkManager {

    public Team PlayersTeam { get; set; }

    public bool IsUnitPlayable(Unit unit) {
        return (Game.Mode == GameMode.HotSeat || unit.Team == PlayersTeam) && unit.Team == Game.CurrentTeam;
    }

}