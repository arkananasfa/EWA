using System;

public class Cooldown {

    public event Action OnStateSet;

    public readonly int Full;

    public int Now { get; protected set; }
    public virtual bool IsReady => Now <= 0;

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