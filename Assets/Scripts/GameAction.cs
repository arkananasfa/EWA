using System.Collections.Generic;

public class GameAction {

    public GameActionType Type { get; set; }

    public Unit Unit { get; set; }
    public Cage Cage { get; set; }
    public int Parameter { get; set; }

    public List<Cage> PossibleTargets { get; set; }

    public override string ToString() {
        return $"Action performing by {(Unit == null ? "player" : Unit.Name)} in [{Cage.X}, {Cage.Y}] with parameter {Parameter}";
    }

}

public enum GameActionType {

    Buy,
    Move,
    Attack,
    Skill,

}