using UnityEngine;

public class UnitsFactory {

    public virtual Unit CreateUnit(UnitType unitType) {
        return unitType switch {
            UnitType.Swordsman => new Swordsman(),
            UnitType.Archer => new Archer(),

            UnitType.HellHound => new HellHound(),
            UnitType.Invoker => new Invoker(),
            UnitType.MeleeImp => new MeleeImp(),
            UnitType.RangedImp => new RangedImp(),
            UnitType.MageImp => new MageImp(),

            UnitType.Butcher => new Butcher(),
            UnitType.Necromancer => new Necromancer(),
            UnitType.Sceleton => new Sceleton(),

            UnitType.AntiMage => new AntiMage(),
            UnitType.SpaceMage => new SpaceMage(),

            UnitType.Hedgehogman => new Hedgehogman(),

            UnitType.Rider => new HorseRider(),
            
            UnitType.Raptor => new Raptor(),

            UnitType.Sniper => new Sniper(),
            
            _ => new Swordsman(),
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