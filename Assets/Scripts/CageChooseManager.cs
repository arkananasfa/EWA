using System.Linq;
using UnityEngine;
using Zenject;

public class CageChooseManager {

    [Inject]
    private GameActionPerformer _performer;

    [Inject]
    private UnitsChooseManager _unitsChooseManager;

    private GameAction _gameAction;

    [Inject]
    public void InjectGameLoop(GameLoop gameLoop) {
        gameLoop.OnMoveEnded += CancelAction;
    }

    public void ChooseCage(Cage cage) {
        if (_gameAction is not null && _gameAction.PossibleTargets.ToList().Contains(cage)) {
            _gameAction.Cage = cage;
            _performer.Perform(_gameAction);
            CancelAction();
        } else if (cage.Unit != null) {
            _unitsChooseManager.ChooseUnit(cage.Unit);
        }
    }

    public void SetAction(GameAction gameAction, bool network = true) {
        if (gameAction.PossibleTargets.Count == 0)
            return;

        CancelAction();

        if (gameAction.PossibleTargets.Count == 1) {
            gameAction.Cage = gameAction.PossibleTargets.First();
            _performer.Perform(gameAction, network);
            CancelAction();
        } else {
            _gameAction = gameAction;
            MarkPossibleTargets();
        }
    }

    public void CancelAction() {
        if (_gameAction == null)
            return;

        UnmarkPossibleTargets();
        _gameAction = null;
    }

    private void MarkPossibleTargets() {
        if (_gameAction == null) 
            return;

        foreach (var cage in _gameAction.PossibleTargets) 
            cage.View.Mark();
    }

    private void UnmarkPossibleTargets() {
        if (_gameAction == null) 
            return;

        foreach (var cage in _gameAction.PossibleTargets)
            cage.View.Unmark();
    }

}