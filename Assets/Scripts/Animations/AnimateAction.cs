using UnityEngine;

public class AnimateAction {

    public AnimatedObject Object { get; set; }
    public AnimatedActionType Type { get; set; }

    public float Time { get; set; }

    public Transform Destination { get; set; }
    public Sprite Sprite { get; set; }
    public float Parameter { get; set; }

    public void Apply() {
        if (Object == null) return;

        switch (Type) {
            case AnimatedActionType.Move:
                ApplyMove();
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
        //Object.MoveTo()
    }

}

public enum AnimatedActionType {

    Move,
    SetSprite,
    SetSize,
    SetRotation,
    Wait

}