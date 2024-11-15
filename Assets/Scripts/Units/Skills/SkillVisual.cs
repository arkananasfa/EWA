﻿using System;
using UnityEngine;

public class SkillVisual {

    public string Name;
    public string Description;
    public Sprite Icon;

    public bool IsVisible;

    public SkillVisual(string name, string description, Sprite icon, bool isVisible = true) {
        Name = name;
        Description = description;
        Icon = icon;
        IsVisible = isVisible;
    }

    public SkillVisual() {
        Name = "";
        Description = "";
        Icon = null;
        IsVisible = false;
    }

    public SkillVisual(NameDescriptionJSON visualJSON) {
        Name = visualJSON.Name;
        Description = visualJSON.Description;
    }

}

[Serializable]
public class NameDescriptionJSON {

    public string Code;
    public string Name;
    public string Description;

}