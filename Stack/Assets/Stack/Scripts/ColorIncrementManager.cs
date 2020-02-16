using UnityEngine;


public class ColorIncrementManager {


    private static readonly float MIN_SATURATION = 0.1f;
    private static readonly float MAX_SATURATION = 0.8f;
    private static readonly float MIN_VALUE = 0.4f;
    private static readonly float MAX_VALUE = 1;

    private bool isIncrementingSaturation;
    private bool isIncrementingValue;


    public Color NewColorFromOther(Color previousColor, float changePercentage) {

        if (changePercentage <= 0) {
            throw new System.ArgumentException("Bad change percentage");
        }

        float h, s, v;
        Color.RGBToHSV(previousColor, out h, out s, out v);

        h += changePercentage;
        if (h > 1) {
            //hue is a circle, so it must be calculated with modulo
            // => https://upload.wikimedia.org/wikipedia/commons/0/00/HSV_color_solid_cone_chroma_gray.png
            h = h % 1;
        }

        UpdateColorPart(ref s, ref isIncrementingSaturation, changePercentage, MIN_SATURATION, MAX_SATURATION);
        UpdateColorPart(ref v, ref isIncrementingValue, changePercentage, MIN_VALUE, MAX_VALUE);

        return Color.HSVToRGB(h, s, v);
    }

    private static void UpdateColorPart(ref float part, ref bool isIncrementingPart, float changePercentage, float min, float max) {

        if (isIncrementingPart) {

            part += changePercentage;

            if (part > max) {
                part = max;
                isIncrementingPart = false;
            }

        } else {

            part -= changePercentage;

            if (part < min) {
                part = min;
                isIncrementingPart = true;
            }
        }
    }

}