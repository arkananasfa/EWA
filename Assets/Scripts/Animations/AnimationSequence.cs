using System;
using System.Collections.Generic;

public class AnimationSequence {

    public static AnimationSequence Current;

    public static AnimationSequence New() {
        Current = new AnimationSequence();
        return Current;
    }

    public static AnimatedObject Add(AnimatedObject ao, Action action) {
        var old = Current;
        New().BeforeAnimation = old.WaitTime;
        Current.Main = ao;
        action();
        Current = old;
        return ao;
    }

    public float WaitTime => Main.SequenceWaitTime + BeforeAnimation;
    public float BeforeAnimation { get; private set; }

    public AnimatedObject Main { get; set; }

    public void AddMain(AnimatedObject main) {
        Main = main;
    }

}