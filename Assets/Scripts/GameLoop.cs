using System;
using System.Collections.Generic;

public class GameLoop {

    public event Action OnMoveEnded;
    public event Action OnMoveStarted;

    public event Action<Unit> OnUnitDied;

    public event Action OnBattleActionHappened;

    public event Action<Unit, Unit, HPInfluence> OnHpInfluenceApplied;

    public void MoveStarted() {
        OnMoveStarted?.Invoke();
        foreach (var unit in Game.CurrentTeam.Units) {
            unit.Skills.ForEach(skill => skill.OnMoveStarted?.Invoke());
        }
        OnBattleActionHappened?.Invoke();
    }

    public void MoveEnded() {
        foreach (var unit in Game.CurrentTeam.Units) {
            unit.Skills.ForEach(skill => skill.OnMoveEnded?.Invoke());
        }
        Game.CurrentPlayer.Gold += Game.CurrentPlayer.GoldPerRound;
        Game.CurrentTeam.ApplyEndMoveMoving();
        OnMoveEnded?.Invoke();
        Game.CurrentTeamSwap();
        MoveStarted();
    }

    public void PreApplyHpInfluence(Unit damager, Unit victim, HPInfluence hpInfluence) {
        Team damagerTeam = damager.Team;
        foreach (var unit in GetUnitsOfTeam(damagerTeam)) {
            unit.Skills.ForEach(skill => skill.OnPreApplyHpInfluence?.Invoke(damager, victim, hpInfluence));
        }
        Team opponentTeam = damagerTeam.Opponent;
        foreach (var unit in GetUnitsOfTeam(opponentTeam)) {
            unit.Skills.ForEach(skill => skill.OnPreApplyHpInfluence?.Invoke(damager, victim, hpInfluence));
        }
    }

    public void HpInfluenceApplied(Unit damager, Unit victim, HPInfluence hpInfluence) {
        Team damagerTeam = damager.Team;
        foreach (var unit in GetUnitsOfTeam(damagerTeam)) {
            if (unit != null)
                unit.Skills.ForEach(skill => skill.OnHpInfluenceApplied?.Invoke(damager, victim, hpInfluence));
        }
        Team opponentTeam = damagerTeam.Opponent;
        foreach (var unit in GetUnitsOfTeam(opponentTeam)) {
            unit.Skills.ForEach(skill => skill.OnHpInfluenceApplied?.Invoke(damager, victim, hpInfluence));
        }
        OnHpInfluenceApplied?.Invoke(damager, victim, hpInfluence);
        OnBattleActionHappened?.Invoke();
    }

    public void UnitUsedSkill(Unit caster, Skill skill) {
        Team casterTeam = caster.Team;
        foreach (var unit in GetUnitsOfTeam(casterTeam)) {
            unit.Skills.ForEach(skill => skill.OnUnitUsedSkill?.Invoke(caster, skill));
        }
        Team opponentTeam = casterTeam.Opponent;
        foreach (var unit in GetUnitsOfTeam(opponentTeam)) {
            unit.Skills.ForEach(skill => skill.OnUnitUsedSkill?.Invoke(caster, skill));
        }
        OnBattleActionHappened?.Invoke();
    }

    public void UnitDied(Unit deadUnit) {
        Team deadUnitTeam = deadUnit.Team;
        foreach (var unit in GetUnitsOfTeam(deadUnitTeam)) {
            unit.Skills.ForEach(skill => skill.OnUnitDied?.Invoke(deadUnit));
        }
        Team opponentTeam = deadUnitTeam.Opponent;
        foreach (var unit in GetUnitsOfTeam(opponentTeam)) {
            unit.Skills.ForEach(skill => skill.OnUnitDied?.Invoke(deadUnit));
        }
        OnUnitDied?.Invoke(deadUnit);
        OnBattleActionHappened?.Invoke();
    }

    public void UnitMoved(Unit unit, Cage from, Cage to) {
        OnBattleActionHappened?.Invoke();
    }

    public List<Unit> GetUnitsOfTeam(Team team) {
        List<Unit> units = new List<Unit>();
        units.AddRange(team.Units);
        return units;
    }

}