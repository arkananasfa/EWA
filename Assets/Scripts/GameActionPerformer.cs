using Zenject;

public class GameActionPerformer {

    [Inject]
    private UnitsFactory _unitsFactory;

    public void Perform(GameAction action) {
        if (action.Type == GameActionType.Buy)
            PerformBuy(action.Cage, (UnitType)action.Parameter);
        if (action.Type == GameActionType.Move)
            PerformMove(action.Unit, action.Cage);
        if (action.Type == GameActionType.Attack)
            PerformAttack(action.Unit, action.Cage);
    }

    private void PerformBuy(Cage cage, UnitType type) {
        Unit newUnit = _unitsFactory.CreateUnit(type);
        newUnit.Cage = cage;
        newUnit.Init(Game.UnitViewSpritesArchive.GetSpriteByUnitType(type), cage, Game.CurrentTeam);
    }

    private void PerformMove(Unit unit, Cage cage) {
        unit.Mover.Move(cage);
    }

    private void PerformAttack(Unit unit, Cage cage) {
        unit.Attacker.Attack(cage);
    }

}