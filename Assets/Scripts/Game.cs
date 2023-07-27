using Zenject;

public static class Game {

    //Teams
    public static Team Team1;
    public static Team Team2;
    public static Team CurrentTeam;

    //Map
    public static Map Map;

    //Game
    public static GameActionBuilder ActionBuilder;
    public static UnitsFactory UnitsFactory;
    public static CageChooseManager CageChooseManager;
    public static GlobalUnitList GlobalUnitList;
    public static GameLoop Loop;

    //Data extraction
    public static SpritesExtractor SpritesExtractor;
    public static UnitViewSpritesArchive UnitViewSpritesArchive;

    public static void CurrentTeamSwap() {
        CurrentTeam = CurrentTeam == Team1 ? Team2 : Team1;
    }
    
    public static void ClearGame() {
        Team1 = null;
        Team2 = null;
        CurrentTeam = null;

        Map = null;

        ActionBuilder = null;
        UnitsFactory = null;
        CageChooseManager = null;
        GlobalUnitList = null;
        Loop = null;
        SpritesExtractor = null;

        UnitViewSpritesArchive = null;
    }

}