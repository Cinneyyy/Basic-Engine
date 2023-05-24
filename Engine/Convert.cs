using System.Globalization;

namespace Engine;

public static class Convert
{
    public static float AsFloat(this int value) => value;
    public static float AsFloat(this int value, float offset) => value + offset;

    public static int AsInt(this float value) => (int)value;
    public static int AsInt(this float value, float offset) => (int)(value + offset);

    public static float AsFloat(this string value) => float.Parse(value, CultureInfo.InvariantCulture);
    public static int AsInt(this string value) => int.Parse(value);
}