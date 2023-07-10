using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Engine;

public readonly struct Vec : IEnumerable<float>, IEquatable<Vec>
{
    public readonly float[] values;
    public readonly int dims;

    public float sqrLength
    {
        get {
            float ret = 0;
            foreach(var f in values)
                ret += f*f;
            return ret;
        }
    }
    public float length => sqrLength.Sqrt();
    public float normalized => values.Sum() / dims;

    #region Pre defined vectors
    public static readonly Vec zero_2d = new(0f, 0f), zero_3d = new(0f, 0f, 0f), zero_4d = new(0f, 0f, 0f, 0f);
    public static readonly Vec one_2d = new(1f, 1f), one_3d = new(1f, 1f, 1f), one_4d = new(1f, 1f, 1f, 1f);
    public static readonly Vec oneHalf_2d = one_2d / 2f, oneHalf_3d = one_3d / 2f, oneHalf_4d = one_4d / 2f;
    public static readonly Vec negativeOne_2d = -one_2d, negativeOne_3d = -one_3d, negativeOne_4d = -one_4d;

    public static readonly Vec right_2d = new(1f, 0f), right_3d = new(1f, 0f, 0f), right_4d = new(1f, 0f, 0f, 0f);
    public static readonly Vec left_2d = -right_2d, left_3d = -right_3d, left_4d = -right_4d;
    public static readonly Vec up_2d = new(0f, 1f), up_3d = new(0f, 1f, 0f), up_4d = new(0f, 1f, 0f, 0f);
    public static readonly Vec down_2d = -up_2d, down_3d = -up_3d, down_4d = -up_4d;

    public static readonly Vec forward_3d = new(0f, 0f, 1f), forward_4d = new(0f, 0f, 1f, 0f);
    public static readonly Vec backward_3d = -forward_3d, backward_4d = -forward_4d;

    public static readonly Vec in_4d = new(0f, 0f, 0f, 1f);
    public static readonly Vec out_4d = -in_4d;
    #endregion


    public Vec(int dims)
    {
        this.dims = dims;
        values = new float[dims];
    }

    public Vec(params float[] values)
    {
        this.values = values;
        dims = values.Length;
    }

    public Vec(int dims, params float[] values)
    {
        if(dims != values.Length)
            throw new Exception("Dimensions of vector do not match the given values.");

        this.dims = dims;
        this.values = values;
    }


    public float this[int index]
    {
        get => values[index];
        set => values[index] = value;
    }
    public float this[Index index]
    {
        get => values[index];
        set => values[index] = value;
    }


    public IEnumerator<float> GetEnumerator()
    {
        foreach(var f in values)
            yield return f;
    }
    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();

    public bool Equals(Vec other)
        => this == other;
    public override bool Equals(object other)
        => (other is Vec) && this == (Vec)other;

    public override int GetHashCode()
        => HashCode.Combine(values.GetHashCode(), dims.GetHashCode());


    public static bool operator ==(Vec a, Vec b)
    {
        if(a.dims != b.dims)
            return false;

        for(int i = 0; i < a.dims; i++)
            if(a[i] != b[i])
                return false;

        return true;
    }
    public static bool operator !=(Vec a, Vec b)
        => !(a == b);

    public static Vec operator +(Vec a, Vec b)
    {
        if(a.dims != b.dims) 
            throw new Exception("Arithmatic operation cannot be applied to vectors of different dimensions.");

        Vec ret = new(a.dims);
        for(int i = 0; i < a.dims; i++)
            ret[i] = a[i] + b[i];

        return ret;
    }
    public static Vec operator -(Vec a, Vec b)
    {
        if(a.dims != b.dims) 
            throw new Exception("Arithmatic operation cannot be applied to vectors of different dimensions.");

        Vec ret = new(a.dims);
        for(int i = 0; i < a.dims; i++)
            ret[i] = a[i] - b[i];

        return ret;
    }
    public static Vec operator *(Vec a, Vec b)
    {
        if(a.dims != b.dims) 
            throw new Exception("Arithmatic operation cannot be applied to vectors of different dimensions.");

        Vec ret = new(a.dims);
        for(int i = 0; i < a.dims; i++)
            ret[i] = a[i] * b[i];

        return ret;
    }
    public static Vec operator /(Vec a, Vec b)
    {
        if(a.dims != b.dims) 
            throw new Exception("Arithmatic operation cannot be applied to vectors of different dimensions.");

        Vec ret = new(a.dims);
        for(int i = 0; i < a.dims; i++)
            ret[i] = a[i] / b[i];

        return ret;
    }

    public static Vec operator *(Vec a, float b)
    {
        Vec ret = new(a.dims);
        for(int i = 0; i < a.dims; i++)
            ret[i] = a[i] * b;

        return ret;
    }
    public static Vec operator /(Vec a, float b)
    {
        Vec ret = new(a.dims);
        for(int i = 0; i < a.dims; i++)
            ret[i] = a[i] / b;

        return ret;
    }

    public static Vec operator -(Vec v)
    {
        Vec ret = new(v.dims);

        for(int i = 0; i < v.dims; i++)
            ret[i] = -v[i];

        return ret;
    }    
    public static Vec operator +(Vec v)
    {
        Vec ret = new(v.dims);

        for(int i = 0; i < v.dims; i++)
            ret[i] = v[i];

        return ret;
    }


    public static explicit operator Vec2(Vec v) => new(v[0], v[1]);
    public static explicit operator Vec(Vec2 v) => new(v.x, v.y);

    public static explicit operator Vec3(Vec v) => new(v[0], v[1], v[2]);
    public static explicit operator Vec(Vec3 v) => new(v.x, v.y, v.z);
}