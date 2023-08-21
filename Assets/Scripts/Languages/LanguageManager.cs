using ModestTree;
using System;
using UnityEngine;

public static class LanguageManager {

    public static event Action OnLanguageChanged;

    private static readonly string[] Languages = new string[]{ "EN" };

    public static string Language {
        get => PlayerPrefs.GetString("Language");
        set {
            PlayerPrefs.SetString("Language", value);
        }
    }

    private static SkillsLanguage _skillsLanguage;
    private static HeroesLanguage _heroesLanguage;

    private static bool _isInited;

    public static void Init() {
        if (_isInited) return;

        if (Language == "")
            Language = Languages[0];

        _skillsLanguage = new();
        _skillsLanguage.Init(Language);

        _heroesLanguage = new();
        _heroesLanguage.Init(Language);
    }

    public static void ChangeLanguage() {
        int currentPosition = Languages.IndexOf(Language);
        Language = Languages[currentPosition + 1 < Languages.Length ? currentPosition + 1 : 0];
        _isInited = false;
        Init();
    }

    public static NameDescriptionJSON GetSkillVisual(string skillCode) {
        return _skillsLanguage.GetSkillVisual(skillCode);
    }

    public static NameDescriptionJSON GetHeroVisual(string heroCode) {
        return _heroesLanguage.GetHeroVisual(heroCode);
    }

}