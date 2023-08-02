public class TsunamiStrike : Skill {

    public TsunamiStrike(Unit unit) : base(unit, "TsunamiStrike", new Cooldown(1)) {
        OnHpInfluenceApplied += ApplyTsunamiStrike;
    }

    private void ApplyTsunamiStrike(Unit attacker, Unit defender, HPInfluence hpInfluence) {
        if (!CanUse())
            return;
        Cooldown.Use();

        if (hpInfluence.Type == HPChangeType.Damage && attacker == owner) {
            CageListBuilder builder = CageListBuilder.New.Use4Neighbor(defender.Cage);
            foreach (var cage in builder.Cages) {
                if (!cage.IsEmpty && cage.Unit.Team != attacker.Team) {
                    if (cage.Unit.HasEffect("DeathPromise"))
                        cage.Unit.ApplyHPChange(owner, owner.Damage);
                    else
                        cage.Unit.ApplyHPChange(owner, HPInfluence.NewDamage(owner.Damage.Value/3, DamageType.Physical, RangeType.Melee));
                }
            }
        }
    }
    
}