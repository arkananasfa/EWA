using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitViewSpritesArchive : MonoBehaviour {

    [SerializeField]
    private UnitView _unitViewPrefab;

    [SerializeField]
    private List<UnitViewSpritesArchiveElement> _elements;

    public Sprite GetSpriteByUnitType(UnitType unitType) {
        Sprite resultSprite = _elements.Where(element => element.unitType == unitType).Select(element => element.sprite).FirstOrDefault();
        if (resultSprite == null)
            throw new Exception($"Sprite for UnitType {unitType} isn't defined.");
        return resultSprite;
    }

    public UnitView GetUnitViewPrefab() {
        return _unitViewPrefab;
    }

}

[Serializable]
public struct UnitViewSpritesArchiveElement {

    public UnitType unitType;
    public Sprite sprite;

}