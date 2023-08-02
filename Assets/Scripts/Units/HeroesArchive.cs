using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeroesArchive : MonoBehaviour {

    [SerializeField]
    private UnitView _unitViewPrefab;

    [SerializeField]
    private List<HeroesArchiveElement> _elements;

    public Sprite GetSpriteByUnitType(HeroType heroType) {
        Sprite resultSprite = _elements.Where(element => element.heroType == heroType).Select(element => element.sprite).FirstOrDefault();
        if (resultSprite == null)
            throw new Exception($"Sprite for UnitType {heroType} isn't defined.");
        return resultSprite;
    }

    public HeroesArchiveElement GetElementByUnitType(HeroType heroType) {
        HeroesArchiveElement element = _elements.Where(element => element.heroType == heroType).FirstOrDefault();
        if (element.heroType != heroType)
            throw new Exception($"Archive element for UnitType {heroType} isn't defined.");
        return element;
    }

    public UnitView GetUnitViewPrefab() {
        return _unitViewPrefab;
    }

}

[Serializable]
public struct HeroesArchiveElement {

    public HeroType heroType;
    public Sprite sprite;

}