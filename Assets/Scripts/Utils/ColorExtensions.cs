using UnityEngine;
using UnityEngine.UI;

public static class ColorExtensions {
    public static void ChangeAlpha (this Color color, float alpha) {
        color.a = alpha;
    }

    public static T ChangeAlpha<T>(this T g, float newAlpha)
        where T : Graphic {
        var color = g.color;
        color.a = newAlpha;
        g.color = color;
        return g;
    }

    public static Texture2D ToBlackAndWhite (this Texture2D texture) {
        int width = texture.width;
        int height = texture.height;
        var resultTexture = new Texture2D(width, height, texture.format, true);

        for (int y = 0;y<height;y++) {
            for (int x = 0;x<width;x++) {
                Color color = texture.GetPixel(x, y);
                float value = (color.r + color.g + color.b) / 3f;
                color = new Color(value, value, value);
                resultTexture.SetPixel(x, y, color);
            }
        }

        return resultTexture;
    }

}