using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

public class Necromancy : Skill {

    public Necromancy(Unit unit) : base(unit, "Necromancy", new Cooldown(1, 1)) {
        OnUnitDied += TryCastNecromancy;
    }

    private void TryCastNecromancy(Unit victim) {
        if (Cooldown.IsReady) {
            if (victim.Team != owner.Team) {
                if (victim.Cage != null && victim.Cage.Unit == null && owner.Cage.IsInRadius(victim.Cage, 2)) {
                    Unit newUnit = Game.UnitsFactory.CreateUnit(UnitType.Sceleton);
                    newUnit.Cage = victim.Cage;
                    var archiveElement = Game.UnitsArchive.GetElementByUnitType(UnitType.Sceleton);
                    newUnit.Init(archiveElement.sprite, victim.Cage, Game.CurrentPlayer);
                }
            }
        }
    }

}