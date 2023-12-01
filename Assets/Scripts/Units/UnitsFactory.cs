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
            UnitType.Fairy => new Fairy(),

            UnitType.Rider => new HorseRider(),
            UnitType.Berserk => new Berserk(),

            UnitType.Raptor => new Raptor(),
            UnitType.Assassin => new Assassin(),

            UnitType.Fishman => new Fishman(),
            UnitType.Whale => new Whale(),

            UnitType.Sniper => new Sniper(),
            UnitType.BeerBarrel => new BeerBarrel(),

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