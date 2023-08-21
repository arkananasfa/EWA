using Zenject;

public class MultiplayerGameActionPerformer : GameActionPerformer {

    [Inject]
    private UnitsFactory _unitsFactory;

    public override void Perform(GameAction action, bool network = true) {
        if (action.Type == GameActionType.HeroPick && Game.Network.IsPlayersNumber(action.X)) {
            Game.Network.SendActionToOpponent(action);
        } else if (network && Game.Network.PlayersTeam == Game.CurrentTeam && action.Type != GameActionType.HeroPick) {
            if (action.Type == GameActionType.MoveEnd)
                Game.Network.SendMoveEndActionToOpponent(action);
            else Game.Network.SendActionToOpponent(action);
        }
        base.Perform(action);
    }

}