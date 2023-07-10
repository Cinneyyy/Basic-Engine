using System;
using sd_col = System.Drawing.Color;

namespace Engine;

public struct Color
{
    public float r, g, b, a;
    public byte rByte
    {
        get => (byte)(r * 255f);
        set => r = value / 255f;
    }
    public byte gByte
    {
        get => (byte)(g * 255f);
        set => g = value / 255f;
    }
    public byte bByte
    {
        get => (byte)(b * 255f);
        set => b = value / 255f;
    }
    public byte aByte
    {
        get => (byte)(a * 255f);
        set => a = value / 255f;
    }

    public static readonly Color red = new(1f, 0f, 0f);
    public static readonly Color green = new(0f, 1f, 0f);
    public static readonly Color blue = new(0f, 0f, 1f);
    public static readonly Color white = new(1f, 1f, 1f);
    public static readonly Color black = new(0f, 0f, 0f);
    public static readonly Color yellow = new(1f, 1f, 0f);
    public static readonly Color orange = new(1f, .5f, 0f);
    public static readonly Color aqua = new(0f, 1f, 1f);
    public static readonly Color pink = new(1f, 0f, 1f);
    public static readonly Color purple = new(.5f, 0f, 1f);
    public static readonly Color gray = new(.5f, .5f, .5f);
    public static readonly Color darkGray = new(.25f, .25f, .25f);
    public static readonly Color transparent = new(0f, 0f, 0f, 0f);


    public Color(float r, float g, float b, float a)
    {
        this.r = r;
        this.g = g;
        this.b = b;
        this.a = a;
    }

    public Color(float r, float g, float b) : this(r, g, b, 1f) { }

    public Color(byte r, byte g, byte b, byte a) : this(r / 255f, g / 255f, b / 255f, a / 255f) { }

    public Color(byte r, byte g, byte b) : this(r, g, b, 255) { }

    public Color(int hexCodeRGB) : this(r: (byte)((hexCodeRGB >> 16) & 0xFF),
                                        g: (byte)((hexCodeRGB >> 8) & 0xFF),
                                        b: (byte)(hexCodeRGB & 0xFF)) { }

    public Color(uint hexCodeRGBA) : this(r: (byte)((hexCodeRGBA >> 24) & 0xFF),
                                          g: (byte)((hexCodeRGBA >> 16) & 0xFF),
                                          b: (byte)((hexCodeRGBA >> 8) & 0xFF),
                                          a: (byte)(hexCodeRGBA & 0xFF)) { }


    public override bool Equals(object obj) => obj is Color color && r == color.r && g == color.g && b == color.b && a == color.a;
    public override int GetHashCode() => HashCode.Combine(r, g, b, a);
    public override string ToString() => $"({r.Ts()}, {g.Ts()}, {b.Ts()}, {a.Ts()})";


    // Based on http://www.splinter.com.au/converting-hsv-to-rgb-colour-using-c/
    public static Color FromHSV(float h, float s, float v)
    {
        while(h < 0)
            h += 360; 
        h %= 360;

        Color ret = new();

        if(v <= 0)
            ret.r = ret.g = ret.b = 0;
        else if(s <= 0)
            ret.r = ret.g = ret.b = v;
        else
        {
            float hf = h/60;
            float
                f = hf - hf.Floor(),
                pv = v * (1 - s),
                qv = v * (1 - s * f),
                tv = v * (1 - s * (1 - f));

            (float r, float g, float b) rgb = hf.Floor() switch {
                0 => (v, tv, pv),
                1 => (qv, v, pv),
                2 => (pv, v, tv),
                3 => (pv, qv, v),
                4 => (tv, pv, v),
                5 or -1 => (v, pv, qv),
                6 => (v, tv, pv),
                _ => (v, v, v)
            };

            ret.r = rgb.r;
            ret.g = rgb.g;
            ret.b = rgb.b;
        }

        return ret;
    }


    public static bool operator ==(Color a, Color b) => a.r == b.r && a.g == b.g && a.b == b.b && a.a == b.a;
    public static bool operator !=(Color a, Color b) => a.r != b.r || a.g != b.g || a.b != b.b || a.a != b.a;

    public static Color operator +(Color a, Color b) => new(a.r + b.r, a.g + b.g, a.b + b.b, a.a + b.a);
    public static Color operator -(Color a, Color b) => new(a.r - b.r, a.g - b.g, a.b - b.b, a.a - b.a);
    public static Color operator /(Color a, Color b) => new(a.r / b.r, a.g / b.g, a.b / b.b, a.a / b.a);
    public static Color operator /(Color a, float b) => new(a.r / b, a.g / b, a.b / b, a.a);
    public static Color operator *(Color a, Color b) => new(a.r * b.r, a.g * b.g, a.b * b.b, a.a * b.a);
    public static Color operator *(Color a, float b) => new(a.r * b, a.g * b, a.b * b, a.a);
    public static Color operator *(float a, Color b) => new(a * b.r, a * b.g, a * b.b, b.a);


    public static implicit operator sd_col(Color c) => sd_col.FromArgb(c.aByte, c.rByte, c.gByte, c.bByte);
    public static implicit operator Color(sd_col c) => new(c.R / 255f, c.G / 255f, c.B / 255f, c.A / 255f);
}