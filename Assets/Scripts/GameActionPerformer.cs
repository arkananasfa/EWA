using UnityEngine;
using Zenject;

public class GameActionPerformer {

    [Inject]
    private UnitsFactory _unitsFactory;

    public virtual void Perform(GameAction action, bool network = true) {
        if (action.Type == GameActionType.Buy)
            PerformBuy(action.Cage, (UnitType)action.Parameter);
        if (action.Type == GameActionType.Move)
            PerformMove(action.Unit, action.Cage);
        if (action.Type == GameActionType.Attack)
            PerformAttack(action.Unit, action.Cage);
        if (action.Type == GameActionType.Skill)
            PerformActiveSkill(action.Unit, action.Parameter, action.Cage);
        if (action.Type == GameActionType.HeroPick)
            PerformHeroPick(action.Cage.X, (HeroType)action.Parameter);
        if (action.Type == GameActionType.MoveEnd)
            PerformMoveEnd();
    }

    protected virtual void PerformBuy(Cage cage, UnitType type) {
        if (cage == null)
            Debug.Log("Cage is null");
        Unit newUnit = _unitsFactory.CreateUnit(type);
        newUnit.Cage = cage;
        var archiveElement = Game.UnitsArchive.GetElementByUnitType(type);
        newUnit.Init(archiveElement.sprite, cage, Game.CurrentPlayer);
        Game.CurrentPlayer.Gold -= archiveElement.price;
    }

    protected virtual void PerformMove(Unit unit, Cage cage) {
        if (unit == null)
            Debug.Log("Unit is null");
        if (cage == null)
            Debug.Log("Cage is null");
        unit.Mover.Apply(cage);
    }

    protected virtual void PerformAttack(Unit unit, Cage cage) {
        if (unit == null)
            Debug.Log("Unit is null");
        if (cage == null)
            Debug.Log("Cage is null");
        unit.Attacker.Apply(cage);
    }

    protected virtual void PerformActiveSkill(Unit unit, int skillNumber, Cage cage) {
        if (unit == null)
            Debug.Log("Unit is null");
        if (cage == null)
            Debug.Log("Cage is null");
        unit.ActiveSkills[skillNumber].Apply(cage);
    }

    protected virtual void PerformHeroPick(int playerNumber, HeroType heroType) {
        Player player = playerNumber == 1 ? Game.Player1 : Game.Player2;
        
        Hero h = Game.UnitsFactory.CreateHero(heroType);
        var archiveElement = Game.HeroesArchive.GetElementByUnitType(heroType);
        h.Init(archiveElement.sprite, Game.Map.GetCage(player.HeroStartCageX, player.HeroStartCageY), player);
        HeroesChooseUI.Instance.SetOverviewPicked(heroType, playerNumber);
        player.IsPicked = true;
        if (Game.Player1.IsPicked && Game.Player2.IsPicked) {
            HeroesChooseUI.Instance.EndHeroPick();
        }
    }

    protected virtual void PerformMoveEnd() {
        Game.Loop.MoveEnded();
    }

}