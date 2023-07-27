using System.Linq;
using Zenject;

public class CageChooseManager {

    [Inject]
    private GameActionPerformer _performer;

    private GameAction _gameAction;

    public void ChooseCage(Cage cage) {
        if (_gameAction == null)
            return;

        if (_gameAction.PossibleTargets.ToList().Contains(cage)) {
            _gameAction.Cage = cage;
            _performer.Perform(_gameAction);
            CancelAction();
        }
    }

    public void SetAction(GameAction gameAction) {
        if (gameAction.PossibleTargets.Count == 0)
            return;

        CancelAction();

        if (gameAction.PossibleTargets.Count == 1) {
            gameAction.Cage = gameAction.PossibleTargets.First();
            _performer.Perform(gameAction);
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
        foreach (var cage in _gameAction.PossibleTargets) 
            cage.View.Mark();
    }

    private void UnmarkPossibleTargets() {
        foreach (var cage in _gameAction.PossibleTargets)
            cage.View.Unmark();
    }

}