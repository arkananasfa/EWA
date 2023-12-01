public class FunAtmosphere : Skill {

    public FunAtmosphere(Unit unit) : base(unit, "FunAtmosphere", Cooldown.NoCooldown) {
        OnPreApplyHpInfluence += Cast;
    }

    private void Cast (Unit attacker, Unit defender, HPInfluence damage) {
        if (attacker.IsTeammate(owner) && attacker.Cage.IsInRadius(owner.Cage, 1) && attacker != owner) {
            damage.Value += 3;
            AnimationContainer.CreateProjectileTogether(owner.Cage, attacker.Cage, "Beer");
        }
    }

}