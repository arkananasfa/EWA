using System;
using Zenject;

public class UnitsChooseManager {

    [Inject]
    private UnitInfoPanel _unitInfoPanel;

    [Inject]
    private UnitsActionsUI _unitsActionsUI;

    public void ChooseUnit(Unit unit) {
        _unitInfoPanel.SetUnit(unit);
        if (Game.Network.IsUnitPlayable(unit)) {
            _unitsActionsUI.SetUnit(unit);
        }
    }
}