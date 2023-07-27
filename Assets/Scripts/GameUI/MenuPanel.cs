using UnityEngine;

public class MenuPanel : MonoBehaviour {

    public void NextMoveButton() {
        Game.CurrentTeamSwap();
    }

}