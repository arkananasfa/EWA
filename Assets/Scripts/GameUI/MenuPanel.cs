using UnityEngine;

public class MenuPanel : MonoBehaviour {

    public void NextMoveButton() {
        if (Game.Network.IsPlayersTurn()) {
            var action = new GameAction();
            action.Type = GameActionType.MoveEnd;
            Game.GameActionPerformer.Perform(action);
        }
    }

    public void ToMenuButton() {

    }

}