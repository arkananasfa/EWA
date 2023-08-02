using UnityEngine;
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
        if (action.Type == GameActionType.Skill)
            PerformActiveSkill(action.Unit, action.Parameter, action.Cage);
    }

    private void PerformBuy(Cage cage, UnitType type) {
        Unit newUnit = _unitsFactory.CreateUnit(type);
        newUnit.Cage = cage;
        var archiveElement = Game.UnitsArchive.GetElementByUnitType(type);
        newUnit.Init(archiveElement.sprite, cage, Game.CurrentPlayer);
        Game.CurrentPlayer.Gold -= archiveElement.price;
    }

    private void PerformMove(Unit unit, Cage cage) {
        unit.Mover.Apply(cage);
    }

    private void PerformAttack(Unit unit, Cage cage) {
        unit.Attacker.Apply(cage);
    }

    private void PerformActiveSkill(Unit unit, int skillNumber, Cage cage) {
        unit.ActiveSkills[skillNumber].Apply(cage);
    }

}