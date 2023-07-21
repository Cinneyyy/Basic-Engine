using R = System.Random;
using COL = Engine.Color;
using System;

namespace Engine;

public static class Random
{
    public static float value
    {
        get {
            lock(syncLock)
                return (float)rand.NextDouble();
        }
    }
    public static int seed
    {
        set => rand = new(value);
    }

    private static R rand = new(0);
    private static readonly object syncLock = new();


    public static float Range(float min, float max)
        => value * (max - min) + min;
    public static int Range(int min, int max)
        => rand.NextLocked(syncLock, min, max);
    public static long Range(long min, long max)
        => (long)(rand.Next((int)(min >> 32), (int)(max >> 32)) << 32) | (long)rand.Next((int)min, (int)max);
    
    public static COL Color(ColorRNGMode rngMode = ColorRNGMode.RandomValues)
    {
        switch(rngMode)
        {
            case ColorRNGMode.RandomValues:
                return new(value, value, value);

            case ColorRNGMode.RandomHex:
                return new(Range(0, 0xFFFFFF+1));

            case ColorRNGMode.Grayscale:
            {
                float val = value;
                return new(val, val, val);
            }

            case ColorRNGMode.NormalizedValues:
            {
                COL col = new(value, value, value);
                return col / (col.r*col.r + col.g*col.g + col.b*col.b).Sqrt();
            }

            case ColorRNGMode.NormalizedHex:
            {
                COL col = new(Range(0, 0xFFFFFF+1));
                return col / (col.r*col.r + col.g*col.g + col.b*col.b).Sqrt();
            }

            case ColorRNGMode.HueOnly:
                return COL.FromHSV(value, 1, 1);

            case ColorRNGMode.Pairs:
            {
                float val = value;
                int combo = val <= .333f ? 0 : val <= .666f ? 1 : 2;
                float ia = value, ib = value;
                float l = (ia*ia + ib*ib).Sqrt();
                float oa = ia/l, ob = ib/l;
                return combo switch {
                    0 => new(oa, ob, 0),
                    1 => new(0, oa, ob),
                    2 => new(ob, 0, oa),
                    _ => COL.white
                };
            }

            default:
                return COL.black;
        }
    }

    public static Vec2 Vec2(Vec2 min, Vec2 max)
        => new(Range(min.x, max.x), Range(min.y, max.y));
    public static Vec2i Vec2(Vec2i min, Vec2i max)
        => new(Range(min.x, max.x), Range(min.y, max.y));

    public static T Select<T>(T a, T b) => value > .5f ? a : b;
    public static T Select<T>(T a, T b, float threshhold) => value > threshhold ? a : b;

    public static int ResetSeed()
    {
        var dt = DateTime.Now;
        return seed = dt.Second * (dt.Millisecond + 69 - dt.Hour) + dt.Day + dt.Millisecond - dt.Microsecond;
    }
}