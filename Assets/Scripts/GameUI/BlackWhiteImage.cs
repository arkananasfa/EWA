using UnityEngine;
using UnityEngine.UI;

public class BlackWhiteImage : RawImage {

    protected override void Start() {
        texture = ((Texture2D)texture).ToBlackAndWhite();
    }

}