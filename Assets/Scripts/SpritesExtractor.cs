using System;
using UnityEngine;

public class SpritesExtractor {

    public Sprite GetSkillSprite(string code) {
        var sprite = Resources.Load<Sprite>($"SkillsSprites/{code}");
        if (sprite is null)
            throw new Exception($"No sprite with code {code}");
        return sprite;
    }

}