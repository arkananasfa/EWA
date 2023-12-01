using System;

public class ShowParameter {

    public event Action<decimal> OnValueChanged;

    private bool _asInt;

    public ShowParameter(decimal value, bool showAsInt)
    {
        Value = value;
        _asInt = showAsInt;
    }

    public decimal Value {
        get {
            if (_asInt)
                return (int)_value;
            else 
                return Math.Round(_value, 1);
        }
        set {
            _value = value;
            OnValueChanged?.Invoke(Value);
        }
    }
    private decimal _value;

}