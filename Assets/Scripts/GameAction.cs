using Mirror;
using System.Collections.Generic;

public class GameAction {

    public GameActionType Type { get; set; }

    public Unit Unit { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int Parameter { get; set; }

    public Cage Cage {
        get => Game.Map.GetCage(X, Y);
        set {
            X = value.X;
            Y = value.Y;
        }
    }

    public GameAction() {}

    public GameAction(NetworkGameAction netAction) {
        if (netAction.unitX != -1) 
            Unit = Game.Map.GetCage(netAction.unitX, netAction.unitY).Unit;
        Parameter = netAction.parameter;
        X = netAction.x;
        Y = netAction.y;
        Type = (GameActionType)netAction.type;
    }

    public List<Cage> PossibleTargets { get; set; }

    public override string ToString() {
        return $"Action performing by {(Unit == null ? "player" : Unit.Name)} in [{Cage.X}, {Cage.Y}] with parameter {Parameter}";
    }

    public NetworkGameAction ToNetwork() {
        return new NetworkGameAction {
            y = Y,
            x = X,
            parameter = Parameter,
            type = (int)Type,
            unitX = Unit == null ? -1 : Unit.Cage.X,
            unitY = Unit == null ? -1 : Unit.Cage.Y,
        };
    }

    public MoveEndGameAction ToMoveEndNetwork() {
        return new MoveEndGameAction {
            teamNumber = Game.CurrentTeam == Game.Team1 ? 2 : 1
        };
    }

}

public struct MoveEndGameAction : NetworkMessage {

    public int teamNumber;

}

public struct NetworkGameAction : NetworkMessage {

    public int type;

    public int unitX;
    public int unitY;
    public int x;
    public int y;
    public int parameter;
    
}

public enum GameActionType {

    Buy,
    Move,
    Attack,
    Skill,
    MoveEnd,
    HeroPick

}