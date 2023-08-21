using System;
using UnityEngine;

public class SpritesExtractor {

    public Sprite GetSkillSprite(string code) {
        var sprite = Resources.Load<Sprite>($"SkillsSprites/{code}");
        if (sprite is null)
            throw new Exception($"No skill's sprite with code {code}");
        return sprite;
    }

    public Sprite GetEffectSprite(string code) {
        var sprite = Resources.Load<Sprite>($"EffectsSprites/{code}");
        if (sprite is null)
            throw new Exception($"No effect's sprite with code {code}");
        return sprite;
    }

    public GameObject GetEffectVisual(string code) {
        var visual = Resources.Load<GameObject>($"EffectsVisuals/{code}");
        if (visual is null)
            throw new Exception($"No effect's visual with code {code}");
        return visual;
    }

    internal GameObject GetAttackVisual(string code) {
        var visual = Resources.Load<GameObject>($"AttackVisuals/{code}");
        if (visual is null)
            throw new Exception($"No attack's visual with code {code}");
        return visual;
    }
}