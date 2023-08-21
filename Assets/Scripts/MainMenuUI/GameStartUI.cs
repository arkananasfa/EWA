using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartUI : MonoBehaviour {

    [SerializeField]
    private NetworkPanel _networkUI;

    public void StartGameWithFriend() {
        GlobalGameSettings.GameMode = GameMode.Multiplayer;
        _networkUI.StartWithFriendGameSearch();
    }

    public void StartHotSeatPlay() {
        GlobalGameSettings.GameMode = GameMode.HotSeat;
        SceneManager.LoadScene("Game");
    }

}