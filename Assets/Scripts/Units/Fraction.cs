using System;
using System.Collections.Generic;

[Serializable]
public class Fraction {

    public List<UnitType> Units;

}

public enum FractionType {

    People,
    Demons,
    Undeads,
    Mages,
    Druids,
    Wanderers

}