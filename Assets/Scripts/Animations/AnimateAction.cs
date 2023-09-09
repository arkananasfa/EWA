/*using UnityEngine;

public class AnimatedAction {

    public AnimatedObject Object { get; set; }
    public AnimatedActionType Type { get; set; }

    public float Time { get; set; }
    public float WaitTime { get; set; }

    public Transform Destination { get; set; }
    public Sprite Sprite { get; set; }
    public float Parameter { get; set; }

    public static AnimatedAction CreateMove(AnimatedObject owner, float waitTime, Transform destination) {
        var aa = new AnimatedAction();
        aa.Object = owner;
        aa.Destination = destination;
        aa.WaitTime = waitTime;
        aa.Time = -1;
        return aa;
    }

    public static AnimatedAction CreateMove(AnimatedObject owner, float waitTime, float time, Transform destination) {
        var aa = new AnimatedAction();
        aa.Object = owner;
        aa.Destination = destination;
        aa.WaitTime = waitTime;
        aa.Time = time;
        return aa;
    }

    public static AnimatedAction CreateUnitMove(AnimatedObject owner, float waitTime, float time, Transform destination) {
        var aa = new AnimatedAction();
        aa.Object = owner;
        aa.Destination = destination;
        aa.WaitTime = waitTime;
        aa.Time = time;
        return aa;
    }

    public void Apply() {
        if (Object == null) return;

        switch (Type) {
            case AnimatedActionType.Move:
                ApplyMove();
                break;
            case AnimatedActionType.MoveUnit:
                ApplyUnitMove();
                break;
            case AnimatedActionType.SetSprite:
                //ApplySetSprite();
                break;
            case AnimatedActionType.SetSize:
                //ApplySetSize();
                break;
            case AnimatedActionType.SetRotation:
                //ApplySetRotation();
                break;
        }
    }

    private void ApplyMove() {
        if (Time == -1)
            Object.ApplyMove(Destination, WaitTime);
        else
            Object.ApplyMove(Destination, WaitTime, Time);
    }

    private void ApplyUnitMove() {
        Object.ApplyMove(Destination, 0, Time);
    }

}

public enum AnimatedActionType {

    Move,
    MoveUnit,
    SetSprite,
    SetSize,
    SetRotation,
    Wait

}*/