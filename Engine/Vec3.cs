using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace Engine;

/// <summary>
/// Represents a 3-dimensional vector with x, y and z components
/// </summary>
#pragma warning disable
public struct Vec3 : IEnumerable<float>, IEquatable<Vec3>
#pragma warning restore
{
    /// <summary>
    /// The "width" axis of the vector, where +x is right and -x is left
    /// </summary>
    public float x;
    /// <summary>
    /// The "height" axis of the vector, where +y is up and -y is down
    /// </summary>
    public float y;
    /// <summary>
    /// The "depth" axis of the vector, where +z is forward and -z is backward
    /// </summary>
    public float z;

    public float length => sqrLength.Sqrt();
    public float sqrLength => x*x + y*y + z*z;
    public Vec3 normalized => this / length;

    public static readonly Vec3 right = new(1, 0, 0);
    public static readonly Vec3 left = -right;
    public static readonly Vec3 up = new(0, 1, 0);
    public static readonly Vec3 down = -up;
    public static readonly Vec3 one = new(1, 1, 1);
    public static readonly Vec3 minusOne = -one;
    public static readonly Vec3 zero = new(0, 0, 0);
    public static readonly Vec3 forward = new(0, 0, 1);
    public static readonly Vec3 backward = -forward;


    public Vec3(float x, float y) : this(x, y, 0f) { } 

    public Vec3(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }


    public float Dist(Vec3 other) => Dist(this, other);

    public float Dot(Vec3 other) => Dot(this, other);
    public float Cross(Vec3 other) => Cross(this, other);

    public float Angle(Vec3 other) => Angle(this, other);
    public float SignedAngle(Vec3 other) => SignedAngle(this, other);

    public Vec3 Lerp(Vec3 other, float t) => Lerp(this, other, t);

    public Vec3 Delta(Vec3 other) => Delta(this, other);

    public Vec3 Dir(Vec3 other) => Dir(this, other);

    public void Set(float x, float y) => this = new(x, y);
    public void SetX(float x) => this.x = x;
    public void SetY(float y) => this.y = y;

    public void Scale(float fac) => this *= fac;
    public void Scale(Vec3 other) => this *= other;

    public void Round() => this = Round(this);
    public void Floor() => this = Floor(this);
    public void Ceil() => this = Ceil(this);

    public void Clamp(float min, float max) => this = Clamp(this, min, max);
    public void Clamp(Vec3 min, Vec3 max) => this = Clamp(this, min, max);

    public void Abs() => this = Abs(this);


    public static float Dist(Vec3 a, Vec3 b) => (b - a).length;

    public static float Dot(Vec3 a, Vec3 b) => a.x*b.x + a.y*b.y + a.z*b.z;
    public static float Cross(Vec3 a, Vec3 b) => a.x*b.x - a.y*b.y - a.z*b.z;

    public static float Angle(Vec3 from, Vec3 to)
        => (Dot(from, to) / (from.sqrLength * to.sqrLength).Sqrt()).Clamp(-1f, 1f).Acos() * Mathf.Rad2Deg;
    public static float SignedAngle(Vec3 from, Vec3 to)
        => Angle(from, to) * Cross(from, to).Sign();

    public static Vec3 Lerp(Vec3 a, Vec3 b, float t) => new(a.x.Lerp(b.x, t), a.y.Lerp(b.y, t), a.z.Lerp(b.z, t));

    public static Vec3 Delta(Vec3 a, Vec3 b) => b - a;

    public static Vec3 Dir(Vec3 a, Vec3 b) => (a - b).normalized;

    public static Vec3 Round(Vec3 v) => new(v.x.Round(), v.y.Round(), v.z.Round());
    public static Vec3 Floor(Vec3 v) => new(v.x.Floor(), v.y.Floor(), v.z.Floor());
    public static Vec3 Ceil(Vec3 v) => new(v.x.Ceil(), v.y.Ceil(), v.z.Ceil());

    public static Vec3 Scale(Vec3 v, float f) => v * f;
    public static Vec3 Scale(Vec3 a, Vec3 b) => a * b;

    public static Vec3 Abs(Vec3 v) => new(v.x.Abs(), v.y.Abs(), v.z.Abs());

    public static Vec3 Clamp(Vec3 v, float min, float max)
        => new(v.x < min ? min : v.x > max ? max : v.x,
               v.y < min ? min : v.y > max ? max : v.y,
               v.z < min ? min : v.z > max ? max : v.z);
    public static Vec3 Clamp(Vec3 v, Vec3 min, Vec3 max)
        => new(v.x < min.x ? min.x : v.x > max.x ? max.x : v.x,
               v.y < min.y ? min.y : v.y > max.y ? max.y : v.y,
               v.z < min.z ? min.z : v.z > max.z ? max.z : v.z);


    public IEnumerator<float> GetEnumerator()
    {
        yield return x;
        yield return y;
        yield return z;
    }
    IEnumerator IEnumerable.GetEnumerator() 
        => GetEnumerator();

    public bool Equals(Vec3 other) 
        => other.x == x && other.y == y && other.z == z;

    public override bool Equals(object obj) 
        => obj is Vec3 vec && x == vec.x && y == vec.y && z == vec.z;
    public override int GetHashCode() 
        => HashCode.Combine(x, y, z);
    public override string ToString() 
        => $"({x.Ts()}, {y.Ts()}, {z.Ts()})";


    public float this[int i]
    {
        get => i switch {
            0 => x,
            1 => y,
            2 => z,
            _ => throw new IndexOutOfRangeException()
        };
        set {
            switch(i)
            {
                case 0: x = value; break;
                case 1: y = value; break;
                case 2: z = value; break;
                default: throw new IndexOutOfRangeException();
            }
        }
    }


    #region Operators
    #region Comparison
    public static bool operator ==(Vec3 l, Vec3 r) => l.x == r.x && l.y == r.y && l.z == r.z;
    public static bool operator !=(Vec3 l, Vec3 r) => l.x != r.x || l.y != r.y || l.z != r.z;
    #endregion

    #region Unary
    public static Vec3 operator +(Vec3 v) => v;
    public static Vec3 operator -(Vec3 v) => new(-v.x, -v.y, -v.z);
    #endregion

    #region Addition, Subtraction, Multiplication & Division
    public static Vec3 operator +(Vec3 l, Vec3 r) => new(l.x + r.x, l.y + r.y, l.z + r.z);
    public static Vec3 operator +(Vec3 l, float r) => new(l.x + r, l.y + r, l.z + r);
    public static Vec3 operator +(float l, Vec3 r) => new(l + r.x, l + r.y, l + r.z);

    public static Vec3 operator -(Vec3 l, Vec3 r) => new(l.x - r.x, l.y - r.y, l.z - l.z);
    public static Vec3 operator -(Vec3 l, float r) => new(l.x - r, l.y - r, l.z - r);
    public static Vec3 operator -(float l, Vec3 r) => new(l - r.x, l - r.y, l - r.z);

    public static Vec3 operator /(Vec3 l, Vec3 r) => new(l.x / r.x, l.y / r.y, l.z / r.z);
    public static Vec3 operator /(Vec3 l, float r) => new(l.x / r, l.y / r, l.z / r);
    public static Vec3 operator /(float l, Vec3 r) => new(l / r.x, l / r.y, l / r.z);

    public static Vec3 operator *(Vec3 l, Vec3 r) => new(l.x * r.x, l.y * r.y, l.z * r.z);
    public static Vec3 operator *(Vec3 l, float r) => new(l.x * r, l.y * r, l.z * r);
    public static Vec3 operator *(float l, Vec3 r) => new(l * r.x, l * r.y, l * r.z);
    #endregion
    #endregion


    public static implicit operator Vec3((float x, float y, float z) t) => new(t.x, t.y, t.z);
    
    public static explicit operator (float x, float y, float z)(Vec3 v) => (v.x, v.y, v.z);
    public static explicit operator Vec3(Vec2 v) => new(v.x, v.y);
    public static explicit operator Vec2(Vec3 v) => new(v.x, v.y);
}