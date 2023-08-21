using System.Collections.Generic;
using UnityEngine;

public class HeroesLanguage {

    private Dictionary<string, NameDescriptionJSON> _heroesNameDescriptions;

    public void Init(string languageCode) {
        if (_heroesNameDescriptions == null)
            _heroesNameDescriptions = new();
        var jsonText = Resources.Load<TextAsset>($"Languages/HeroesDescriptions/{languageCode}");
        string json = jsonText.text;
        NameDescriptionJSON[] heroesVisuals = JSONService.ReadJSON<NameDescriptionJSON>(json);
        foreach (var skillVisual in heroesVisuals) {
            _heroesNameDescriptions[skillVisual.Code] = skillVisual;
        }
    }

    public NameDescriptionJSON GetHeroVisual(string heroCode) {
        return _heroesNameDescriptions[heroCode];
    }

}