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

    public override void InstallBindings() {
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

        // Bind GameActionPerformer
        GameActionPerformer gameActionPerformer = new GameActionPerformer();
        Container.BindInstance(gameActionPerformer).AsSingle();

        // Bind GameActionBuilder
        GameActionBuilder gameActionBuilder = new GameActionBuilder();
        Container.BindInstance(gameActionBuilder).AsSingle();

        // Bind UnitsFactory
        UnitsFactory unitsFactory = new UnitsFactory();
        Container.BindInstance(unitsFactory).AsSingle();

        // Bind UnitInfoPanel
        Container.Bind<UnitInfoPanel>().FromComponentInHierarchy().AsSingle();

        // Bind SpritesExtractor
        SpritesExtractor spritesExtractor = new SpritesExtractor();
        Container.BindInstance(spritesExtractor).AsSingle();

        Container.QueueForInject(globalUnitList);
        Container.QueueForInject(gameLoop);
        Container.QueueForInject(gameActionPerformer);
        Container.QueueForInject(cageChooseManager);
        Container.QueueForInject(gameActionBuilder);
        Container.QueueForInject(spritesExtractor);

        Game.GlobalUnitList = globalUnitList;
        Game.CageChooseManager = cageChooseManager;
        Game.ActionBuilder = gameActionBuilder;
        Game.UnitsFactory = unitsFactory;
        Game.Map = map;
        Game.Loop = gameLoop;
        Game.SpritesExtractor = spritesExtractor;

        Team team1 = new Team(-1);
        Team team2 = new Team(1);
        team1.Color = _team1Color;
        team2.Color = _team2Color;
        Game.CurrentTeam = team1;
        Game.Team1 = team1;
        Game.Team2 = team2;

        Game.UnitViewSpritesArchive = FindObjectOfType<UnitViewSpritesArchive>();
    }

    private Map CreateMap() {
        return new Map(_width, _height, _cagesParent, _cageViewPrefab);
    }

}