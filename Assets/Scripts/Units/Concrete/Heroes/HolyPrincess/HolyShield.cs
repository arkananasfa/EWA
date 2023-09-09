public class HolyShield : Skill {

    public HolyShield(Unit unit) : base(unit, "HolyShield", Cooldown.NoCooldown) {
        OnPreApplyHpInfluence += DefendUnit;
    }

    private void DefendUnit(Unit attacker, Unit defender, HPInfluence hpInfluence) {
        if (hpInfluence.Type == HPChangeType.Damage && defender.Team == owner.Team && owner.Cage.IsInRadius(defender.Cage, 2)) { 
            hpInfluence.Value *= 0.8m;
        }
    }

}