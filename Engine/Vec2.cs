using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace Engine;

public struct Vec2 : IEnumerable<float>, IEquatable<Vec2>
{
    /// <summary>
    /// The "width" axis of the vector, where +x is right and -x is left
    /// </summary>
    public float x;
    /// <summary>
    /// The "height" axis of the vector, where +y is up and -y is down
    /// </summary>
    public float y;

    public float sqrLength => x*x + y*y;
    public float length => sqrLength.Sqrt();
    public Vec2 normalized => this / length;

    public static readonly Vec2 left = new(-1, 0);
    public static readonly Vec2 right = new(1, 0);
    public static readonly Vec2 up = new(0, 1);
    public static readonly Vec2 down = new(0, -1);
    public static readonly Vec2 one = new(1, 1);
    public static readonly Vec2 negativeOne = new(-1, -1);
    public static readonly Vec2 zero = new(0, 0);
    public static readonly Vec2 oneHalf = one / 2f;


    public Vec2(float x, float y)
    {
        this.x = x;
        this.y = y;
    }


    public float LerpXY(float t)
        => x.Lerp(y, t);
    public float RandXY()
        => Random.Range(x, y);

    public float Dist(Vec2 other) => Dist(this, other);

    public float Dot(Vec2 other) => Dot(this, other);
    public float Cross(Vec2 other) => Cross(this, other);

    public float Angle(Vec2 other) => Angle(this, other);
    public float SignedAngle(Vec2 other) => SignedAngle(this, other);

    public Vec2 Lerp(Vec2 other, float t) => Lerp(this, other, t);

    public Vec2 Delta(Vec2 other) => Delta(this, other);

    public Vec2 Dir(Vec2 other) => Dir(this, other);

    public void Set(float x, float y) => this = new(x, y);
    public void SetX(float x) => this.x = x;
    public void SetY(float y) => this.y = y;

    public void Scale(float fac) => this *= fac;
    public void Scale(Vec2 other) => this *= other;

    public void Round() => this = Round(this);
    public void Floor() => this = Floor(this);
    public void Ceil() => this = Ceil(this);

    public void Clamp(float min, float max) => this = Clamp(this, min, max);
    public void Clamp(Vec2 min, Vec2 max) => this = Clamp(this, min, max);

    public void Rotate(float deg) => this = Rotate(this, deg);


    public static float Dist(Vec2 a, Vec2 b) => (b - a).length;

    public static float Dot(Vec2 a, Vec2 b) => a.x*b.x + a.y*b.y;
    public static float Cross(Vec2 a, Vec2 b) => a.x*b.x - a.y*b.y;

    public static float Angle(Vec2 from, Vec2 to)
        => (Dot(from, to) / (from.sqrLength * to.sqrLength).Sqrt()).Clamp(-1f, 1f).Acos() * Mathf.Rad2Deg;
    public static float SignedAngle(Vec2 from, Vec2 to)
        => Angle(from, to) * (from.x * to.y - from.y * to.x).Sign();

    public static Vec2 Lerp(Vec2 a, Vec2 b, float t) => new(a.x.Lerp(b.x, t), a.y.Lerp(b.y, t));

    public static Vec2 Delta(Vec2 a, Vec2 b) => b - a;

    public static Vec2 Dir(Vec2 a, Vec2 b) => (a - b).normalized;

    public static Vec2i Round(Vec2 v) => new(v.x.Round(), v.y.Round());
    public static Vec2i Floor(Vec2 v) => new(v.x.Floor(), v.y.Floor());
    public static Vec2i Ceil(Vec2 v) => new(v.x.Ceil(), v.y.Ceil());

    public static Vec2 Scale(Vec2 v, float f) => v * f;
    public static Vec2 Scale(Vec2 a, Vec2 b) => a * b;

    public static Vec2 Clamp(Vec2 v, float min, float max)
        => new(v.x < min ? min : v.x > max ? max : v.x,
               v.y < min ? min : v.y > max ? max : v.y);
    public static Vec2 Clamp(Vec2 v, Vec2 min, Vec2 max)
        => new(v.x < min.x ? min.x : v.x > max.x ? max.x : v.x,
               v.y < min.y ? min.y : v.y > max.y ? max.y : v.y);

    public static Vec2 Rotate(Vec2 v, float deg)
    {
        float rad = Mathf.Pi * deg / 180f, cos = rad.Cos(), sin = rad.Sin();
        return new(v.x * cos - v.y * sin, v.x * sin + v.y * cos);
    }

    public static RectangleF BuildRect(Vec2 pos, Vec2 scale) => new(pos.x, pos.y, scale.x, scale.y);


    public IEnumerator<float> GetEnumerator()
    {
        yield return x;
        yield return y;
    }
    IEnumerator IEnumerable.GetEnumerator() 
        => GetEnumerator();

    public bool Equals(Vec2 other) 
        => other.x == x && other.y == y;

    public override bool Equals(object obj) 
        => obj is Vec2 vec && x == vec.x && y == vec.y;
    public override int GetHashCode() 
        => HashCode.Combine(x, y);
    public override string ToString() 
        => $"({x.Ts()}, {y.Ts()})";


    public float this[int i]
    {
        get => i == 0 ? x : i == 1 ? y : 0;
        set {
            switch(i)
            {
                case 0: x = value; break;
                case 1: y = value; break;
                default: break;
            }
        }
    }


    public static bool operator ==(Vec2 l, Vec2 r) => l.x == r.x && l.y == r.y;
    public static bool operator !=(Vec2 l, Vec2 r) => l.x != r.x || l.y != r.y;

    public static Vec2 operator -(Vec2 v) => new(-v.x, -v.y);
    public static Vec2 operator +(Vec2 v) => v;

    public static Vec2 operator +(Vec2 l, Vec2 r) => new(l.x + r.x, l.y + r.y);
    public static Vec2 operator +(Vec2 l, float r) => new(l.x + r, l.y + r);
    public static Vec2 operator -(Vec2 l, Vec2 r) => new(l.x - r.x, l.y - r.y);
    public static Vec2 operator -(Vec2 l, float r) => new(l.x - r, l.y - r);
    public static Vec2 operator /(Vec2 l, Vec2 r) => new(l.x / r.x, l.y / r.y);
    public static Vec2 operator /(Vec2 l, float r) => new(l.x / r, l.y / r);
    public static Vec2 operator *(Vec2 l, Vec2 r) => new(l.x * r.x, l.y * r.y);
    public static Vec2 operator *(Vec2 l, float r) => new(l.x * r, l.y * r);
    public static Vec2 operator *(float l, Vec2 r) => new(l * r.x, l * r.y);


    public static implicit operator Vec2(SizeF s) => new(s.Width, s.Height);
    public static implicit operator Vec2(PointF s) => new(s.X, s.Y);
    public static implicit operator Vec2((float x, float y) t) => new(t.x, t.y);

    public static explicit operator SizeF(Vec2 v) => new(v.x, v.y);
    public static explicit operator PointF(Vec2 v) => new(v.x, v.y);
    public static explicit operator (float x, float y)(Vec2 v) => (v.x, v.y);
}