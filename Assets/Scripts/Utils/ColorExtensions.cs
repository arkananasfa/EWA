using UnityEngine;

public static class ColorExtensions {
    public static void ChangeAlpha (this Color color, float alpha) { 
    
        color = new Color(color.r, color.g, color.b, alpha);
    
    }
}