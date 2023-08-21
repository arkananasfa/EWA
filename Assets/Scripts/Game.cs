using Zenject;

public static class Game {

    public static GameMode Mode { get; set; }

    //Teams
    public static Player Player1 { get; set; }
    public static Player Player2 { get; set; }
    public static Player CurrentPlayer { get; set; }
    public static Team Team1 => Player1.Team;
    public static Team Team2 => Player2.Team;
    public static Team CurrentTeam => CurrentPlayer.Team;

    //Map
    public static Map Map { get; set; }

    //Game
    public static NetworkController Network { get; set; }
    public static GameActionBuilder ActionBuilder { get; set; }
    public static UnitsFactory UnitsFactory { get; set; }
    public static CageChooseManager CageChooseManager { get; set; }
    public static GameActionPerformer GameActionPerformer { get; set; }
    public static GlobalUnitList GlobalUnitList { get; set; }
    public static GameLoop Loop { get; set; }

    //Client settings
    public static AudioManager AudioManager { get; set; }

    //Data extraction
    public static SpritesExtractor SpritesExtractor { get; set; }
    public static SoundsExtractor SoundsExtractor { get; set; }
    public static UnitsArchive UnitsArchive { get; set; }
    public static HeroesArchive HeroesArchive { get; set; }
    public static FractionsArchive FractionsArchive { get; set; }

    public static DescriptionPanel DescriptionPanel { get; set; }

    public static void CurrentTeamSwap() {
        CurrentPlayer = CurrentPlayer == Player1 ? Player2 : Player1;
    }
    
    public static void ClearGame() {
        Player1 = null;
        Player2 = null;
        CurrentPlayer = null;

        Map = null;

        ActionBuilder = null;
        UnitsFactory = null;
        CageChooseManager = null;
        GlobalUnitList = null;
        Loop = null;

        SpritesExtractor = null;
        UnitsArchive = null;

        Player1.EndGame();
        Player2.EndGame();
    }

}

public enum GameMode {

    Singleplayer,
    HotSeat,
    Multiplayer

}