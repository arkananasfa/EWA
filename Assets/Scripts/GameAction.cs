using System.Collections.Generic;

public class GameAction {

    public GameActionType Type { get; set; }

    public Unit Unit { get; set; }
    public Cage Cage { get; set; }
    public int Parameter { get; set; }

    public List<Cage> PossibleTargets { get; set; }

}

public enum GameActionType {

    Buy,
    Move,
    Attack,
    Skill,

}