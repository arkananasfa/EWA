using System;
using UnityEngine;

public static class JSONService {

    public static T[] ReadJSON<T> (string jsonText) {
        var wrapper = JsonUtility.FromJson<Wrapper<T>>(jsonText);
        return wrapper.Items;
    }

    [Serializable]
    public class Wrapper<T> {
        public T[] Items;
    }

}