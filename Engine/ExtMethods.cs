using System.Globalization;
using System.Windows.Forms;
using SYS = System;

namespace Engine;

public static class ExtMethods
{
    private static readonly NumberFormatInfo numberFormatting = new() {
        NumberDecimalSeparator = "."
    };


    public static string Ts(this float f)
        => f.ToString(numberFormatting);
    public static string Ts(this float f, string format)
        => f.ToString(format, numberFormatting);
    public static string Ts<T>(this T t)
        => t.ToString();

    public static string TsArr<T>(this T[] arr, string seperator = "")
    {
        string ret = "";
        for(int i = 0; i < arr.Length; i++)
            ret += arr[i].Ts() + (i != arr.Length-1 ? seperator : "");
        return ret;
    }

    public static string Join(this string[] s, string seperator)
    {
        string ret = "";

        for(int i = 0; i < s.Length; i++)
            ret += s[i] + (i != s.Length-1 ? seperator : "");

        return ret;
    }

    public static int NextLocked(this SYS::Random rand, object syncLock, int min, int max)
    {
        lock(syncLock)
            return rand.Next(min, max);
    }

    public static TextFormatFlags ToTextFormatFlags(this Alignment alignment)
        => alignment switch {
            Alignment.Top | Alignment.Left or Alignment.TopLeft => TextFormatFlags.Top | TextFormatFlags.Left,
            Alignment.Top | Alignment.YCenter => TextFormatFlags.Top | TextFormatFlags.VerticalCenter,
            Alignment.Top | Alignment.Right => TextFormatFlags.Top | TextFormatFlags.Right,

            Alignment.XCenter | Alignment.Left => TextFormatFlags.HorizontalCenter | TextFormatFlags.Left,
            Alignment.XCenter | Alignment.YCenter or Alignment.Center => TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter,
            Alignment.XCenter | Alignment.Right => TextFormatFlags.HorizontalCenter | TextFormatFlags.Right,

            Alignment.Bottom | Alignment.Left => TextFormatFlags.Bottom | TextFormatFlags.Left,
            Alignment.Bottom | Alignment.YCenter => TextFormatFlags.Bottom | TextFormatFlags.VerticalCenter,
            Alignment.Bottom | Alignment.Right => TextFormatFlags.Bottom | TextFormatFlags.Right,

            _ => TextFormatFlags.Left | TextFormatFlags.Top
        } | TextFormatFlags.GlyphOverhangPadding;
}