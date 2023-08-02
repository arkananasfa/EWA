using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitsArchive : MonoBehaviour {

    [SerializeField]
    private UnitView _unitViewPrefab;

    [SerializeField]
    private List<UnitsArchiveElement> _elements;

    public Sprite GetSpriteByUnitType(UnitType unitType) {
        Sprite resultSprite = _elements.Where(element => element.unitType == unitType).Select(element => element.sprite).FirstOrDefault();
        if (resultSprite == null)
            throw new Exception($"Sprite for UnitType {unitType} isn't defined.");
        return resultSprite;
    }

    public int GetPriceByUnitType(UnitType unitType) {
        int result = _elements.Where(element => element.unitType == unitType).Select(element => element.price).FirstOrDefault();
        if (result == 0)
            throw new Exception($"Price for UnitType {unitType} isn't defined.");
        return result;
    }

    public UnitsArchiveElement GetElementByUnitType(UnitType unitType) {
        UnitsArchiveElement element = _elements.Where(element => element.unitType == unitType).FirstOrDefault();
        if (element.unitType != unitType)
            throw new Exception($"Archive element for UnitType {unitType} isn't defined.");
        return element;
    }

    public UnitView GetUnitViewPrefab() {
        return _unitViewPrefab;
    }

}

[Serializable]
public struct UnitsArchiveElement {

    public UnitType unitType;
    public Sprite sprite;
    public int price;

}