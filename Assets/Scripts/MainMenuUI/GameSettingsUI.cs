using UnityEngine;

public class GameSettingsUI : MonoBehaviour {

    private Player player1 => GlobalGameSettings.Player1;
    private Player player2 => GlobalGameSettings.Player2;

    private void Start() {
        GlobalGameSettings.IsHeroPickingActive = true;

        GlobalGameSettings.Player1 = new Player("a");
        GlobalGameSettings.Player2 = new Player("b");

        player1.AllowAllFractions();
        player2.AllowAllFractions();

        player1.StartGold = 50;
        player2.StartGold = 50;

        player1.MaxHP = 100;
        player2.MaxHP = 100;

        player1.MaxGold = 200;
        player2.MaxGold = 200;

        player1.GoldPerRound = 50;
        player2.GoldPerRound = 50;

        player1.HeroStartCageX = 0;
        player1.HeroStartCageY = 6;

        player2.HeroStartCageX = 7;
        player2.HeroStartCageY = 1;

        player1.IsPicked = false;
        player2.IsPicked = false;
    }

}