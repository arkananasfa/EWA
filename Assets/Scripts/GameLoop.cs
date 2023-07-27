using UnityEngine;
using Zenject;

public class GameLoop : IGameEventInvoker {

    public void MoveStarted() {
        foreach (var unit in Game.CurrentTeam.Units) {
            unit.Skills.ForEach(skill => skill.OnMoveStarted?.Invoke());
        }
    }

    public void MoveEnded() {
        foreach (var unit in Game.CurrentTeam.Units) {
            unit.Skills.ForEach(skill => skill.OnMoveEnded?.Invoke());
        }
        Game.CurrentTeamSwap();
        MoveStarted();
    }

    public void PreApplyHpInfluence(Unit damager, Unit victim, HPInfluence hpInfluence) {
        Team damagerTeam = damager.Team;
        foreach (var unit in damagerTeam.Units) {
            unit.Skills.ForEach(skill => skill.OnPreApplyHpInfluence?.Invoke(damager, victim, hpInfluence));
        }
        Team opponentTeam = damagerTeam.Opponent;
        foreach (var unit in opponentTeam.Units) {
            unit.Skills.ForEach(skill => skill.OnPreApplyHpInfluence?.Invoke(damager, victim, hpInfluence));
        }
    }

    public void HpInfluenceApplied(Unit damager, Unit victim, HPInfluence hpInfluence) {
        Team damagerTeam = damager.Team;
        foreach (var unit in damagerTeam.Units) {
            unit.Skills.ForEach(skill => skill.OnHpInfluenceApplied?.Invoke(damager, victim, hpInfluence));
        }
        Team opponentTeam = damagerTeam.Opponent;
        foreach (var unit in opponentTeam.Units) {
            unit.Skills.ForEach(skill => skill.OnHpInfluenceApplied?.Invoke(damager, victim, hpInfluence));
        }
    }

    public void UnitUsedSkill(Unit caster, Skill skill) {
        Team casterTeam = caster.Team;
        foreach (var unit in casterTeam.Units) {
            unit.Skills.ForEach(skill => skill.OnUnitUsedSkill?.Invoke(caster, skill));
        }
        Team opponentTeam = casterTeam.Opponent;
        foreach (var unit in opponentTeam.Units) {
            unit.Skills.ForEach(skill => skill.OnUnitUsedSkill?.Invoke(caster, skill));
        }
    }

    public void UnitDied(Unit deadUnit) {
        Team deadUnitTeam = deadUnit.Team;
        foreach (var unit in deadUnitTeam.Units) {
            unit.Skills.ForEach(skill => skill.OnUnitDied?.Invoke(deadUnit));
        }
        Team opponentTeam = deadUnitTeam.Opponent;
        foreach (var unit in opponentTeam.Units) {
            unit.Skills.ForEach(skill => skill.OnUnitDied?.Invoke(deadUnit));
        }
    }

    public void UnitMoved(Unit unit, Cage from, Cage to) {

    }



}