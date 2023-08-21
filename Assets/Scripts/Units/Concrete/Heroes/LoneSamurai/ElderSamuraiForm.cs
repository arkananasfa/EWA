using System.Collections.Generic;

public class ElderSamuraiForm : ActiveSkill {

    public ElderSamuraiForm(Unit unit) : base(unit, "ElderSamuraiForm", new Cooldown(8, 0)) {
        applyEffect += CastDeathPromise;
    }

    private void CastDeathPromise(Cage cage) {
        owner.HP = owner.MaxHP;
        Effect effect = new Effect(owner, "ElderSamuraiForm", 4, Effect.Power.Undispellable, Effect.Purpose.Good, () => {
            owner.HP = owner.MaxHP;
            owner.Armor += 3;
            owner.Resistance += 3;
            owner.View.SetSprite(Visual.Icon);
        }).AddEndEffect(() => {
            owner.Armor -= 3;
            owner.Resistance -= 3;
            owner.View.SetSprite(Game.HeroesArchive.GetSpriteByUnitType(HeroType.LoneSamurai));
        });
    }

    protected override List<Cage> GetPossibleCages() {
        return CageListBuilder.New.UseCage(owner.Cage).Cages;
    }

}