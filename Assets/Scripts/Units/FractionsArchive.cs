using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FractionsArchive : MonoBehaviour {

    [SerializeField]
    private List<FractionsArchiveElement> _elements;

    public FractionsArchiveElement GetElementByFractionType(FractionType fractionType) {
        FractionsArchiveElement element = _elements.Where(element => element.fractionType == fractionType).FirstOrDefault();
        if (element.fractionType != fractionType)
            throw new Exception($"Archive element for FractionType {fractionType} isn't defined.");
        return element;
    }

    public List<FractionsArchiveElement> GetAllFractions() {
        return _elements;
    }

}

[Serializable]
public struct FractionsArchiveElement {

    public FractionType fractionType;
    public Fraction fraction;

}