using UnityEngine;

public class UnitsFactory {

    public virtual Unit CreateUnit(UnitType unitType) {
        return unitType switch {
            UnitType.Swordsman => new Swordsman(),
            UnitType.Archer => new Archer(),
            UnitType.HellHound => new HellHound(),
            UnitType.Rider => new HorseRider(),
            UnitType.Invoker => new Invoker(),
            
            UnitType.MeleeImp => new MeleeImp(),
            UnitType.RangedImp => new RangedImp(),
            UnitType.MageImp => new MageImp(),
            _ => new HellHound()
        };
    }

    public virtual Hero CreateHero(HeroType heroType) {
        return heroType switch {
            HeroType.HolyPrincess => new HolyPrincess(),
            HeroType.LoneSamurai => new LoneSamurai(),
            _ => new HolyPrincess()
        };
    }

}