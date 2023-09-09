using UnityEngine;
using Zenject;

public class GameContext : MonoInstaller {

    [Header("Map size")]
    [SerializeField]
    private int _width;

    [SerializeField]
    private int _height;

    [Space(10)]

    [Header("Cages settings")]
    [SerializeField]
    private Transform _cagesParent;

    [SerializeField]
    private CageView _cageViewPrefab;

    [SerializeField]
    private Transform _unitsParent;
    
    [Space(10)]

    [Header("Teams settings")]
    [SerializeField]
    private Color _team1Color;

    [SerializeField]
    private Color _team2Color;

    [SerializeField]
    private PlayerUI _player1UI;

    [SerializeField]
    private PlayerUI _player2UI;

    [SerializeField]
    private Sprite _defaultPlayer1Sprite;

    [SerializeField]
    private Sprite _defaultPlayer2Sprite;

    public override void InstallBindings() {
        LanguageManager.Init();

        Game.Mode = GlobalGameSettings.GameMode;
        if (GlobalGameSettings.Player1 == null)
            SetDefaultPlayers();
        else {
            Game.Player1 = GlobalGameSettings.Player1;
            Game.Player2 = GlobalGameSettings.Player2;
        }
        Game.CurrentPlayer = Game.Player1;

        Team team1 = new Team(-1);
        Team team2 = new Team(1);
        team1.Color = _team1Color;
        team2.Color = _team2Color;

        Game.Player1.Team = team1;
        Game.Player2.Team = team2;

        // Bind GlobalUnitList
        GlobalUnitList globalUnitList = new GlobalUnitList();
        Container.BindInstance(globalUnitList).AsSingle();

        // Bind GameLoop
        GameLoop gameLoop = new GameLoop();
        Container.BindInstance(gameLoop).AsSingle();

        // Bind Map
        Map map = CreateMap();
        Container.BindInstance(map).AsSingle();

        // Bind CageChooseManager
        CageChooseManager cageChooseManager = new CageChooseManager();
        Container.BindInstance(cageChooseManager).AsSingle();

        // Bind CageChooseManager
        UnitsChooseManager unitsChooseManager = new UnitsChooseManager();
        Container.BindInstance(unitsChooseManager).AsSingle();

        // Bind GameActionPerformer
        GameActionPerformer gameActionPerformer = CreateGameActionPerformer();
        Container.BindInstance(gameActionPerformer).AsSingle();

        // Bind GameActionBuilder
        GameActionBuilder gameActionBuilder = new GameActionBuilder();
        Container.BindInstance(gameActionBuilder).AsSingle();

        // Bind UnitsFactory
        UnitsFactory unitsFactory = new UnitsFactory();
        Container.BindInstance(unitsFactory).AsSingle();

        // Bind NetworkManager
        NetworkController networkManager = new NetworkController();
        Container.BindInstance(networkManager).AsSingle();

        // Bind AudioManager
        AudioManager audioManager = new AudioManager();
        Container.BindInstance(audioManager).AsSingle();

        // Bind UnitInfoPanel
        Container.Bind<UnitInfoPanel>().FromComponentInHierarchy().AsSingle();

        // Bind UnitInfoPanel
        Container.Bind<UnitsActionsUI>().FromComponentInHierarchy().AsSingle();

        // Bind SpritesExtractor
        SpritesExtractor spritesExtractor = new SpritesExtractor();
        Container.BindInstance(spritesExtractor).AsSingle();

        // Bind SoundsExtractor
        SoundsExtractor soundsExtractor = new();
        Container.BindInstance(soundsExtractor).AsSingle();

        Container.QueueForInject(globalUnitList);
        Container.QueueForInject(gameLoop);
        Container.QueueForInject(gameActionPerformer);
        Container.QueueForInject(cageChooseManager);
        Container.QueueForInject(gameActionBuilder);
        Container.QueueForInject(spritesExtractor);
        Container.QueueForInject(unitsChooseManager);

        Game.GlobalUnitList = globalUnitList;
        Game.CageChooseManager = cageChooseManager;
        Game.GameActionPerformer = gameActionPerformer;
        Game.ActionBuilder = gameActionBuilder;
        Game.UnitsFactory = unitsFactory;
        Game.Map = map;
        Game.Loop = gameLoop;
        Game.SpritesExtractor = spritesExtractor;
        Game.SoundsExtractor = soundsExtractor;
        Game.Network = networkManager;
        Game.AudioManager = audioManager;
        Game.UnitsArchive = FindObjectOfType<UnitsArchive>();
        Game.HeroesArchive = FindObjectOfType<HeroesArchive>();
        Game.FractionsArchive = FindObjectOfType<FractionsArchive>();
        Game.DescriptionPanel = FindObjectOfType<DescriptionPanel>();

        _player1UI.SetPlayer(Game.Player1);
        _player2UI.SetPlayer(Game.Player2);
    }

    private void SetDefaultPlayers() {
        GlobalGameSettings.IsHeroPickingActive = true;
        Game.Mode = GameMode.HotSeat;
        Game.Player1 = new Player("DungeonMaster");
        Game.Player2 = new Player("Slave");
        Game.Player1.HeroStartCageX = 0;
        Game.Player2.HeroStartCageX = 7;
        Game.Player1.HeroStartCageY = 6;
        Game.Player2.HeroStartCageY = 1;
        Game.Player1.MaxGold = 500;
        Game.Player2.MaxGold = 500;
        _player1UI.SetAvatar(_defaultPlayer1Sprite);
        _player2UI.SetAvatar(_defaultPlayer2Sprite);
    }

    private Map CreateMap() {
        return new Map(_width, _height, _cagesParent, _cageViewPrefab);
    }

    private GameActionPerformer CreateGameActionPerformer() {
        if (Game.Mode == GameMode.Multiplayer)
            return new MultiplayerGameActionPerformer();
        return new GameActionPerformer();
    }

}