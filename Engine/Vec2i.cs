using System;
using System.Drawing;

namespace Engine;

#pragma warning disable IDE0064 // Make readonly fields writable
public struct Vec2i
#pragma warning restore IDE0064
{
    public int x, y;

    public static readonly Vec2 left = new(-1, 0);
    public static readonly Vec2 right = new(1, 0);
    public static readonly Vec2 up = new(0, 1);
    public static readonly Vec2 down = new(0, -1);
    public static readonly Vec2 one = new(1, 1);
    public static readonly Vec2 negativeOne = new(-1, -1);
    public static readonly Vec2 zero = new(0, 0);


    public Vec2i(int x, int y)
    {
        this.x = x;
        this.y = y;
    }


    public void Set(int x, int y) => this = new(x, y);
    public void SetX(int x) => this.x = x;
    public void SetY(int y) => this.y = y;

    public void Scale(int fac) => this *= fac;
    public void Scale(Vec2i other) => this *= other;

    public float RandXY()
        => Random.Range(x, y);


    public override bool Equals(object obj) => obj is Vec2i vec && x == vec.x && y == vec.y;
    public override int GetHashCode() => HashCode.Combine(x, y);
    public override string ToString() => $"({x}, {y})";


    public static bool operator ==(Vec2i l, Vec2i r) => l.x == r.x && l.y == r.y;
    public static bool operator !=(Vec2i l, Vec2i r) => l.x != r.x || l.y != r.y;

    public static Vec2i operator +(Vec2i v) => v;
    public static Vec2i operator -(Vec2i v) => new(-v.x, -v.y);

    public static Vec2i operator +(Vec2i l, Vec2i r) => new(l.x + r.x, l.y + r.y);
    public static Vec2i operator +(Vec2i l, int r) => new(l.x + r, l.y + r);
    public static Vec2i operator -(Vec2i l, Vec2i r) => new(l.x - r.x, l.y - r.y);
    public static Vec2i operator -(Vec2i l, int r) => new(l.x - r, l.y - r);
    public static Vec2i operator /(Vec2i l, Vec2i r) => new(l.x / r.x, l.y / r.y);
    public static Vec2i operator /(Vec2i l, int r) => new(l.x / r, l.y / r);
    public static Vec2i operator *(Vec2i l, Vec2i r) => new(l.x * r.x, l.y * r.y);
    public static Vec2i operator *(Vec2i l, int r) => new(l.x * r, l.y * r);
    public static Vec2i operator *(int l, Vec2i r) => new(l * r.x, l * r.y);


    public static implicit operator Size(Vec2i v) => new(v.x, v.y);
    public static implicit operator Vec2i(Size s) => new(s.Width, s.Height);

    public static implicit operator Vec2i(Point p) => new(p.X, p.Y);
    public static implicit operator Point(Vec2i v) => new(v.x, v.y);

    public static implicit operator Vec2(Vec2i v) => new(v.x, v.y);
    public static explicit operator Vec2i(Vec2 v) => new(v.x.Round(), v.y.Round());

    public static implicit operator Vec2i((int x, int y) t) => new(t.x, t.y);
}