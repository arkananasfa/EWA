public class Hero : Unit {

    public Hero(int hp, string name, HPInfluence damage, int armor, int resistance) : base(hp, name, damage, armor, resistance) {
        Name = LanguageManager.GetHeroVisual(Code).Name;
    }

}