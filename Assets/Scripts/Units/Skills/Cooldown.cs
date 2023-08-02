using System;

public class Cooldown {

    public event Action OnStateSet;

    public readonly int Full;

    public int Now {
        get => Math.Clamp(_now, 0, int.MaxValue);
        set => _now = value;
    }
    public virtual bool IsReady => Now <= 0;

    #region Ready patterns
    public static Cooldown NoCooldown => new Cooldown(0, 0);
    #endregion

    private int _now;

    public Cooldown(int full, int now = 0) {
        Full = full;
        Now = now;
    }

    public virtual void Refresh() {
        Now = 0;
        StateSet();
    }

    public virtual void Use() {
        Now = Full;
        StateSet();
    }

    public virtual void Decrease() {
        Now--;
        StateSet();
    }

    protected void StateSet() {
        OnStateSet?.Invoke();
    }

}