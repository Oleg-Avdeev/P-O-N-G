using UnityEngine;

// Thanks https://www.ronja-tutorials.com/2019/04/16/hsv-colorspace.html
public static class ColorUtility
{
    public static Color HSVtoRGB(float h, float s, float v)
    {
        h = h % 1.0f;
        float r = Mathf.Lerp(1, Mathf.Abs(h * 6 - 3) - 1, s);
        float g = Mathf.Lerp(1, 2 - Mathf.Abs(h * 6 - 2), s);
        float b = Mathf.Lerp(1, 2 - Mathf.Abs(h * 6 - 4), s);
        Color rgb = new Color(r*v,g*v,b*v,1);
        return rgb;
    }

    public static Vector3 RGBtoHSV(float r, float g, float b)
    {
        float maxComponent = Mathf.Max(r, Mathf.Max(g, b));
        float minComponent = Mathf.Min(r, Mathf.Min(g, b));
        float diff = maxComponent - minComponent;
        float hue = 0;
        if (diff == 0)
        {
            hue = 0;
        }
        else if (maxComponent == r)
        {
            hue = 0 + (g - b) / diff;
        }
        else if (maxComponent == g)
        {
            hue = 2 + (b - r) / diff;
        }
        else if (maxComponent == b)
        {
            hue = 4 + (r - g) / diff;
        }
        hue = (hue / 6) % 1.0f;

        float saturation = diff / maxComponent;
        float value = maxComponent;

        return new Vector3(hue, saturation, value);
    }
}