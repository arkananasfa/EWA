using System;

public class UnitShowParameter {

    public event Action<decimal> OnValueChanged;

    public decimal Value {
        get => _value;
        set {
            _value = value;
            OnValueChanged?.Invoke(value);
        }
    }
    private decimal _value;

}