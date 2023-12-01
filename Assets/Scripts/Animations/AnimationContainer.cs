using System;
using UnityEngine;

public class AnimationContainer {

    public static AnimationContainer Current { get; private set; }
    public static AnimationContainer TakeCurrent => Current;
    public static int Index = 0;
    public int index;

    private event Action OnReleased;

    public AnimationContainer previous;
    public float startWaitTime;

    private Action<AnimatedObject> _animatedActions;
    private Func<AnimatedObject> _createAnimatedObject;

    public AnimationContainer() {
        index = Index;
        Index++;
    }

    public static void Create(Func<AnimatedObject> createAnimatedObject, Action realActions, Action<AnimatedObject> animatedActions) {

        AnimationContainer container = new AnimationContainer();
        container._animatedActions = animatedActions;
        container._createAnimatedObject = createAnimatedObject;

        if (Current == null) {
            Current = container;
            realActions();

            Current = null;
            Index = 0;

            container.StartAnimationActions();
        } else {
            container.previous = Current;
            container.previous.OnReleased += container.StartAnimationActions;

            Current = container;

            realActions();

            Current = container.previous;
        }
    }

    public static void CreateProjectile(Cage from, Cage to, Unit owner, Unit defender, HPInfluence hpInfluence, string code, float speed = -1) {
        UnitView defenderView = defender.View;
        Create(() => AnimatedObject.CreateProjectileAt(from.View, code),
                                  () => {
                                      defender.ApplyHPChange(owner, hpInfluence);
                                  },
                                  ao => ao.LikeAttack(to, defenderView, speed)
        );
    }

    public static void CreateProjectileWait(Cage from, Cage to, Unit owner, Unit defender, HPInfluence hpInfluence, string code, float waitTime = 0, float speed = -1) {
        UnitView defenderView = defender.View;
        Create(() => AnimatedObject.CreateProjectileAt(from.View, code).HideFor(waitTime).WaitFor(waitTime),
                                  () => {
                                      TakeCurrent.SetStartWaitTime(waitTime);
                                      defender.ApplyHPChange(owner, hpInfluence);
                                  },
                                  ao => ao.LikeAttack(to, defenderView, speed)
        );
    }

    public static void CreateProjectile(Cage from, Cage to, string code, float speed = -1) {
        Create(() => AnimatedObject.CreateProjectileAt(from.View, code),
                                  () => { },
                                  ao => ao.LikeAttack(to, speed)
        );
    }

    public static void CreateTogether(Func<AnimatedObject> createAnimatedObject, Action realActions, Action<AnimatedObject> animatedActions) {

        AnimationContainer container = new AnimationContainer();
        container._animatedActions = animatedActions;
        container._createAnimatedObject = createAnimatedObject;

        container.previous = Current;
        if (container.previous != null && container.previous.previous != null) {
            container.previous.previous.OnReleased += container.StartAnimationActionsTogether;

            Current = container;

            realActions();

            Current = container.previous;
        } else {
            Current = container;

            realActions();

            Current = container.previous;

            container.StartAnimationActionsTogether();
        }
    }

    public static void CreateProjectileTogether(Cage from, Cage to, string code, float speed = -1) {
        CreateTogether(() => AnimatedObject.CreateProjectileAt(from.View, code),
                                  () => { },
                                  ao => ao.LikeAttack(to, speed)
        );
    }

    public void SetStartWaitTime(float time) {
        startWaitTime = time;
    }

    public void Release() {
        OnReleased?.Invoke();
    }

    private void StartAnimationActions() {
        var animatedObject = _createAnimatedObject().InContainer(this);
        _animatedActions(animatedObject);
    }

    private void StartAnimationActionsTogether() {
        if (previous == null) {
            StartAnimationActions();
            return;
        }
        var animatedObject = _createAnimatedObject().HideFor(previous.startWaitTime).WaitFor(previous.startWaitTime).InContainer(this);
        _animatedActions(animatedObject);
    }
}