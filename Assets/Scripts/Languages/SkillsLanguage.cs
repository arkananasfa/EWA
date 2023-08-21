using System.Collections.Generic;
using UnityEngine;

public class SkillsLanguage {

    private Dictionary<string, NameDescriptionJSON> _skillsNameDescriptions;

    public void Init(string languageCode) {
        if (_skillsNameDescriptions == null) 
            _skillsNameDescriptions = new();
        var jsonText = Resources.Load<TextAsset>($"Languages/SkillsDescriptions/{languageCode}");
        string json = jsonText.text;
        NameDescriptionJSON[] skillsVisuals = JSONService.ReadJSON<NameDescriptionJSON>(json); 
        foreach (var skillVisual in skillsVisuals) {
            _skillsNameDescriptions[skillVisual.Code] = skillVisual;
        }
    }

    public NameDescriptionJSON GetSkillVisual(string skillCode) {
        return _skillsNameDescriptions[skillCode];
    }

}