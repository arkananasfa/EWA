public interface IGameEventInvoker {

    public void MoveStarted();

    public void MoveEnded();

    public void PreApplyHpInfluence(Unit damager, Unit victim, HPInfluence hpInfluence);

    public void HpInfluenceApplied(Unit damager, Unit victim, HPInfluence hpInfluence);

    public void UnitUsedSkill(Unit caster, Skill skill);

    public void UnitMoved(Unit unit, Cage from, Cage to);

    public void UnitDied(Unit unit);

}