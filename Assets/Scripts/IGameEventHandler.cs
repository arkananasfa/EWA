using System;

public interface IGameEventHandler {

    public Action OnMoveStarted { get; set; }
    public Action OnMoveEnded { get; set; }
    public Action<Unit, Unit, HPInfluence> OnPreApplyHpInfluence { get; set; }
    public Action<Unit, Unit, HPInfluence> OnHpInfluenceApplied { get; set; }
    public Action<Unit, Skill> OnUnitUsedSkill { get; set; }
    public Action<Unit, Cage, Cage> OnUnitMoved { get; set; }
    public Action<Unit> OnUnitDied { get; set; }

}