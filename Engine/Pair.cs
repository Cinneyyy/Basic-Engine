using System;
using System.Collections;
using System.Collections.Generic;
using ENG = Engine;

namespace Engine;

public class Pair<T> : IEnumerable<T>
{
    public T a, b;


    public Pair(T a, T b)
    {
        this.a = a;
        this.b = b;
    }

    public Pair(T values) : this(values, values) { }


    public T this[int index]
    {
        get => index switch {
            0 => a,
            1 => b,
            _ => throw new IndexOutOfRangeException()
        };
        set {
            switch(index)
            {
                case 0: a = value; break;
                case 1: b = value; break;
                default: throw new IndexOutOfRangeException();
            }
        }
    }

    public T this[bool selectA]
    {
        get => this[selectA ? 0 : 1];
        set => this[selectA ? 0 : 1] = value;
    }


    public T Random() => ENG::Random.Select(a, b);


    public IEnumerator<T> GetEnumerator()
    {
        yield return a;
        yield return b;
    }
    IEnumerator IEnumerable.GetEnumerator() 
        => GetEnumerator();


    public static implicit operator Pair<T>((T, T) tTuple) => new(tTuple.Item1, tTuple.Item2);
}