public class HPInfluence {

    public decimal Value { get; set; }

    public HPChangeType Type { get; set; }
    public DamageType DamageType { get; set; }
    public HealType HealType { get; set; }
    public RangeType RangeType { get; set; }

    public bool IsBlocked { get; set; }

    public HPInfluence Copy() {
        return new() {
            Value = Value,
            Type = Type,
            DamageType = DamageType,
            HealType = HealType,
            RangeType = RangeType,
            IsBlocked = IsBlocked
        };
    }

    public static HPInfluence NewDamage(decimal value, DamageType type, RangeType range) {
        var hpi = new HPInfluence();
        hpi.Value = value;
        hpi.Type = HPChangeType.Damage;
        hpi.DamageType = type;
        hpi.RangeType = range;
        return hpi;
    }
    
    public static HPInfluence NewHeal(decimal value, HealType type = HealType.Standard) {
        var hpi = new HPInfluence();
        hpi.Value = value;
        hpi.Type = HPChangeType.Heal;
        hpi.HealType = type;
        return hpi;
    }

}

public enum HPChangeType {

    Heal,
    Damage

}

public enum DamageType {

    Physical,
    Magical,
    Absolute

}

public enum RangeType {

    Melee,
    Ranged

}

public enum HealType {

    Standard,
    Overwhelming

}