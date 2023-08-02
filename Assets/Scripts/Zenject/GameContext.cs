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

    public override void InstallBindings() {
        TestGameSetup();

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
        GameActionPerformer gameActionPerformer = new GameActionPerformer();
        Container.BindInstance(gameActionPerformer).AsSingle();

        // Bind GameActionBuilder
        GameActionBuilder gameActionBuilder = new GameActionBuilder();
        Container.BindInstance(gameActionBuilder).AsSingle();

        // Bind UnitsFactory
        UnitsFactory unitsFactory = new UnitsFactory();
        Container.BindInstance(unitsFactory).AsSingle();

        // Bind NetworkManager
        NetworkManager networkManager = new NetworkManager();
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

        Container.QueueForInject(globalUnitList);
        Container.QueueForInject(gameLoop);
        Container.QueueForInject(gameActionPerformer);
        Container.QueueForInject(cageChooseManager);
        Container.QueueForInject(gameActionBuilder);
        Container.QueueForInject(spritesExtractor);
        Container.QueueForInject(unitsChooseManager);

        Game.GlobalUnitList = globalUnitList;
        Game.CageChooseManager = cageChooseManager;
        Game.ActionBuilder = gameActionBuilder;
        Game.UnitsFactory = unitsFactory;
        Game.Map = map;
        Game.Loop = gameLoop;
        Game.SpritesExtractor = spritesExtractor;
        Game.Network = networkManager;
        Game.AudioManager = audioManager;
        Game.UnitsArchive = FindObjectOfType<UnitsArchive>();
        Game.HeroesArchive = FindObjectOfType<HeroesArchive>();

        GameSetup();
    }

    private void TestGameSetup() {
        GlobalGameSettings.MaxHP = 100;
        GlobalGameSettings.MaxGold = 200;
        GlobalGameSettings.GoldPerRound = 50;
        GlobalGameSettings.StartGold = 60;
    }

    private Map CreateMap() {
        return new Map(_width, _height, _cagesParent, _cageViewPrefab);
    }

    private void GameSetup() {
        Player player1 = new Player("Валерій Залужний");
        Player player2 = new Player("Попуск");
        Game.CurrentPlayer = player1;
        Game.Player1 = player1;
        Game.Player2 = player2;

        Team team1 = new Team(-1);
        Team team2 = new Team(1);
        team1.Color = _team1Color;
        team2.Color = _team2Color;

        player1.Team = team1;
        player2.Team = team2;

        _player1UI.SetPlayer(player1);
        _player2UI.SetPlayer(player2);

        Game.Mode = GameMode.HotSeat;

        SpawnHero(Game.Map.GetCage(0, 6), HeroType.HolyPrincess, player1);
        SpawnHero(Game.Map.GetCage(7, 1), HeroType.LoneSamurai, player2);
    }

    private void SpawnHero(Cage cage, HeroType heroType, Player player) {
        Hero h = Game.UnitsFactory.CreateHero(heroType);
        h.Cage = cage;
        var archiveElement = Game.HeroesArchive.GetElementByUnitType(heroType);
        h.Init(archiveElement.sprite, cage, player);
    }

}