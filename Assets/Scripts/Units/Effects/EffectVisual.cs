using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Animator))]
public class EffectVisual : MonoBehaviour {

    private static readonly string EndAnimationName = "End";
    private static readonly string StartAnimationName = "Start";
    private static readonly string UseAnimationName = "Use";
    private static readonly int StartAnimation = Animator.StringToHash(StartAnimationName);
    private static readonly int UseAnimation = Animator.StringToHash(UseAnimationName);
    private static readonly int EndAnimation = Animator.StringToHash(EndAnimationName);

    private Animator GetAnimator {
        get {
            if (_animator == null) {
                _animator = GetComponent<Animator>();
            }
            return _animator;
        }
    }

    private Animator _animator;

    public void StartIn() {
        GetAnimator.Play(StartAnimation);
    }

    public void Use() {
        GetAnimator.Play(UseAnimation);
    }

    public void End() {
        GetAnimator.Play(EndAnimation);
        Invoke(nameof(KillThis), 2);
    }

    public void KillThis() {
        Destroy(gameObject);
    }

}