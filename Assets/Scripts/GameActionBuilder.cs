using System.Collections.Generic;
using UnityEngine;

public class GameActionBuilder {

    public GameAction CreateBuyAction(UnitType unitType) {
        GameAction action = new GameAction();
        action.Type = GameActionType.Buy;
        action.Parameter = (int)unitType;
        int rowNumber = Mathf.RoundToInt(3.5f - Game.CurrentTeam.frontDirection * 3.5f);
        action.PossibleTargets = CageListBuilder.New.UseRow(rowNumber).OnlyEmpty().Cages;
        return action;
    }

    public GameAction CreateMoveAction(Unit unit, List<Cage> cages) {
        GameAction action = new GameAction();
        action.Type = GameActionType.Move;
        action.Unit = unit;
        action.PossibleTargets = cages;
        return action;
    }

    public GameAction CreateAttackAction(Unit unit, List<Cage> cages) {
        GameAction action = new GameAction();
        action.Type = GameActionType.Attack;
        action.Unit = unit;
        action.PossibleTargets = cages;
        return action;
    }

}